﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CuaHangSach.Model
{
    public class PaymentModel
    {
        public string Token { get; set; }

        public long Amount { get; set; }

        public string Description { get; set; }
    }
}
