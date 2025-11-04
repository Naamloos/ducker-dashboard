using System;
using System.Collections.Generic;

namespace Dev.Naamloos.Ducker.Entities
{
    public class Container
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Image { get; set; }
        public string ImageId { get; set; }
        public string Command { get; set; }
        public string Created { get; set; }
        public Dictionary<string, string> Labels { get; set; }
        public string State { get; set; }
    }
}
