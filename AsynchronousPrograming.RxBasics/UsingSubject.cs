using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousPrograming.RxBasics
{
    public class UsingSubject
    {
        public Subject<int> GetNewSubject()
        {
            return new Subject<int>();
        }
    }
}
