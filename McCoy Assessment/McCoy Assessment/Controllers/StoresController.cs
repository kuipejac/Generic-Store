using McCoy_Assessment.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace McCoy_Assessment.Controllers
{
    public class StoresController : Controller
    {
        private static string filePath;
        private static List<StoreModel> v_stores;
        public ActionResult Index()
        {
            filePath = HostingEnvironment.ApplicationPhysicalPath+"Data\\Stores.csv";
            v_stores = new List<StoreModel>();

            using (var sr = new StreamReader(filePath))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    var record = line.Split(',');
                    var tempStore = new StoreModel();
                    int StoreNumber = 0;
                    if (int.TryParse(record[0], out StoreNumber))
                        tempStore.StoreNumber = StoreNumber;
                    else
                        tempStore.StoreNumber = null;
                    tempStore.StoreName = record[1];
                    tempStore.StoreManagerName = record[2];
                    var time = new DateTime();
                    if (DateTime.TryParse(record[3], out time))
                        tempStore.OpenTime = time;
                    else
                        tempStore.OpenTime = null;
                    time = new DateTime();
                    if (DateTime.TryParse(record[4], out time))
                        tempStore.CloseTime = time;
                    else
                        tempStore.CloseTime = null;

                    if(tempStore.StoreNumber.HasValue && !string.IsNullOrEmpty(tempStore.StoreName))
                        v_stores.Add(tempStore);
                    line = sr.ReadLine();
                }
            }
            return View("StoresList", v_stores);
        }

        public ActionResult Details(int StoreNumber)
        {
            return View("StoreDetail", v_stores.Where(x => x.StoreNumber == StoreNumber).FirstOrDefault());
        }

        public ActionResult Edit(int StoreNumber)
        {
            return View(v_stores.Where(x=>x.StoreNumber == StoreNumber).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(StoreModel model)
        {
            v_stores.Remove(v_stores.Where(x => x.StoreNumber == model.StoreNumber).FirstOrDefault());
            v_stores.Add(model);
            Save();
            return RedirectToAction("Index");
        }

        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(StoreModel model)
        {
            v_stores.Add(model);
            Save();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int StoreNumber)
        {
            return View(v_stores.Where(x => x.StoreNumber == StoreNumber).FirstOrDefault());
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(int StoreNumber)
        {
            v_stores.Remove(v_stores.Where(x => x.StoreNumber == StoreNumber).FirstOrDefault());
            Save();
            return RedirectToAction("Index");
        }

        private void Save()
        {
            using (var sw = new StreamWriter(filePath))
            {
                foreach (var store in v_stores)
                {
                    sw.WriteLine(store.StoreNumber+","+store.StoreName+"," + store.StoreManagerName + "," + store.OpenTime + "," + store.CloseTime);
                }
            }
        }
    }
}