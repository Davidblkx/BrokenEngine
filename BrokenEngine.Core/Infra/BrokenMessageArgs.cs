using System;

namespace BrokenEngine.Core
{
    public class BrokenMessageArgs
    {
        public BrokenMessageArgs()
        {
            Category = -1;
            Message = "";
        }

        public long Category { get; set; }
        public object Message { get; set; }
    }

    public delegate void BrokenMessageEvent(object sender, BrokenMessageArgs e);
}
