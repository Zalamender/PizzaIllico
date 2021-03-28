using Newtonsoft.Json;
using System.Collections.Generic;

namespace PizzaIllico.Mobile.Dtos.Pizzas
{
    public class CreateOrderRequest
    {
        [JsonProperty("pizza_ids")]
        public List<long> PizzaIds { get; set; }
    }
}