using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Infrastructure.Dtos
{
    public class Menu
    {
        public string label { get; set; }

        public IList<MainMenuItems> main { get; set; }
    }
    public class MainMenuItems
    {
        public string state { get; set; }

        public string short_label { get; set; }

        public string main_state { get; set; }

        public bool? target { get; set; }

        public string name { get; set; }

        public string type { get; set; }

        public string icon { get; set; }


        public IList<ChildrenItems> children { get; set; }
    }

    public class ChildrenItems
    {
        public string state { get; set; }

        public bool target { get; set; }

        public string name { get; set; }

        public string type { get; set; }

        public IList<ChildrenItems> children { get; set; }


    }
}
