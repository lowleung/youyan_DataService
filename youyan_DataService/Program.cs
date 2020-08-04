using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace youyan_DataService
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new EF.MyDbContext())
            {
                Console.WriteLine("油烟检测数据整理，开始执行....");
                Console.WriteLine("过滤时间设定为前一天。");
                DateTime start = DateTime.Parse( DateTime.Now.AddDays(-1).ToString("d"));
                Console.Write("开始时间为：");
                Console.WriteLine(start);
                DateTime end = start.AddDays(1);
                Console.Write("结束时间为：");
                Console.WriteLine(end);
                Console.WriteLine("查询数据中....");
                Dictionary<string, item> Dic = new Dictionary<string, item>(); 
                foreach (var state  in db.Device_States.Where(x => x.date > start && end > x.date).OrderBy(x => x.date))
                {
                    if (!Dic.ContainsKey(state.code)) {
                        Dic.Add(state.code, new item()
                        {
                            fanDuration = 0,
                            fanLastState = false,
                            fanLastTime = DateTime.Now,
                            purifyDuration = 0,
                            purifyLastTime = DateTime.Now,
                            purifyLastState = false
                        });  
                    }
                    if (state.fan_state)
                    {
                        if (!Dic[state.code].fanLastState)
                        {
                            Dic[state.code].fanLastState = true;
                            Dic[state.code].fanLastTime = state.date;
                        }
                    }
                    else
                    {
                        if (Dic[state.code].fanLastState)
                        {
                            Dic[state.code].fanLastState = false;
                            Dic[state.code].fanDuration += (int)(state.date - Dic[state.code].fanLastTime).TotalSeconds;
                        }
                    }
                    if (state.purify_state)
                    {
                        if (!Dic[state.code].purifyLastState)
                        {
                            Dic[state.code].purifyLastState = true;
                            Dic[state.code].purifyLastTime = state.date;
                        }
                    }
                    else
                    {
                        if (Dic[state.code].purifyLastState)
                        {
                            Dic[state.code].purifyLastState = false;
                            Dic[state.code].purifyDuration += (int)(state.date - Dic[state.code].purifyLastTime).TotalSeconds;
                        }
                    }
                }
                Console.WriteLine("查询数据完成,归档到数据库中...");
                foreach(string key in Dic.Keys)
                {
                    db.Working_Durations.Add(new Model.working_duration()
                    {
                        code = key,
                        fan_duration=Dic[key].fanDuration,
                        purify_duration=Dic[key].purifyDuration,
                        date=start,
                        createdAt=DateTime.Now,
                        updatedAt=DateTime.Now
                    });
                }
                db.SaveChanges();
                Console.WriteLine("归档完成！");
                Thread.Sleep(2000);
            }
        }
    }
    
    public class item {
        public int fanDuration { get; set; }
        public DateTime fanLastTime { get; set; }
        public bool fanLastState { get; set; }
        public int purifyDuration { get; set; }
        public DateTime purifyLastTime { get; set; }
        public bool purifyLastState { get; set; }
    }
}
