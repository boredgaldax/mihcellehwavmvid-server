﻿using Mihcelle.Hwavmvid.Data;

namespace Mihcelle.Hwavmvid.Tasks
{
    public interface IHostedservicebase
    {

        public string Id { get; set; }
        public string Taskname { get; set; }
        public string Projectname { get; set; }
        public bool Active { get; set; }
        public int Interval { get; set; }
        public Task Runtaskimplementation(Applicationdbcontext frameworkapplicationdbcontext);

    }
}
