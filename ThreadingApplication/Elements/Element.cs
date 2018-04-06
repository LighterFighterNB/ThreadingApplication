﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreadingApplication.Elements;

namespace ThreadingApplication
{
    abstract class Element
    {
        protected String title;
        protected Database bd;
        protected AlphaApiFactory apiFactory = new AlphaApiFactory();
        protected AlphaManager api;
    }
}
