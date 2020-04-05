using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class ShapedEntity
    {
        public ShapedEntity()
        {}

        public string Id { get; set; }

        public Entity Entity { get; set; } = new Entity();
    }
}
