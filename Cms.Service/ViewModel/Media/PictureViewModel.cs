using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Service.ViewModel.Media
{
    public class PictureViewModel
    {
        public int Id { get; set; }
        public string MimeType { get; set; }

        /// <summary>
        /// Gets or sets the SEO friendly filename of the picture
        /// </summary>
        public string SeoFilename { get; set; }

        /// <summary>
        /// Gets or sets the "alt" attribute for "img" HTML element. If empty, then a default rule will be used (e.g. product name)
        /// </summary>
        public string AltAttribute { get; set; }

        /// <summary>
        /// Gets or sets the "title" attribute for "img" HTML element. If empty, then a default rule will be used (e.g. product name)
        /// </summary>
        public string TitleAttribute { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the picture is new
        /// </summary>
        public bool IsNew { get; set; }

        /// <summary>
        /// Gets or sets the picture virtual path
        /// </summary>
        public string VirtualPath { get; set; }
    }
}
