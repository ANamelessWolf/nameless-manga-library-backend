using AutoMapper.Internal;
using Nameless.WebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.ConstrainedExecution;

namespace Nameless.WebApi.Models
{

    /// <summary>
    /// OptionalOrderRequest
    /// </summary>
    public class OptionalOrderRequest
    {
        public string? orderField { get; set; }
        public OrderType orderType { get; set; }
        public OptionalOrderRequest()
        {
            this.orderType = OrderType.Asc;
        }
    }
}
