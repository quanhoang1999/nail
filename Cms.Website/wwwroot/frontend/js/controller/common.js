var common = {
    init: function () {
        common.registerEvents();
    },
    registerEvents: function () {
      
        //$('#btnPrint').off('click').on('click', function (e) {
        //    var divContents = document.getElementById("btnPrint").innerHTML;
        //    var printWindow = window.open('', '', 'height=1000,width=700');
        //    printWindow.document.write('<html><head><title>Print DIV Content</title>');
        //    printWindow.document.write('</head><body >');
        //    printWindow.document.write(divContents);
        //    printWindow.document.write('</body></html>');
        //    printWindow.document.close();
        //    printWindow.print();
     
        //});
        function ImagetoPrint(source) {
            return "<html><head><script>function step1(){\n" +
                "setTimeout('step2()', 10);}\n" +
                "function step2(){window.print();window.close()}\n" +
                "</scri" + "pt></head><body onload='step1()'>\n" +
                "<img src='" + source + "' /></body></html>";
        }
        function PrintImage(source) {
            Pagelink = "about:blank";
            var pwa = window.open(Pagelink, "_new");
            pwa.document.open();
            pwa.document.write(ImagetoPrint(source));
            pwa.document.close();
            pwa.print();
        }
    }
}
common.init();