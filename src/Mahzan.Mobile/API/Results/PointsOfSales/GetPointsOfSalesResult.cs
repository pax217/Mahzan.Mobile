﻿using Mahzan.Mobile.API.Results._Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Results.PointsOfSales
{
    public class GetPointsOfSalesResult:Result
    {
        public List<Entities.PointsOfSales> PointsOfSales { get; set; }
    }
}
