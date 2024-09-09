function initTinyMCE(Id) {
    tinyMCE.init({
        // General options
        mode: "exact",
        elements: Id,
        theme: "advanced",
        plugins: "autolink,lists,spellchecker,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template",

        width: "100%",
        height: "800",
        // Theme options
        theme_advanced_buttons1: "save,newdocument,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,styleselect,formatselect,fontselect,fontsizeselect",
        theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,addImg,cleanup,help,code,|,insertdate,inserttime,preview,|,forecolor,backcolor",
        theme_advanced_buttons3: "tablecontrols,|,hr,removeformat,visualaid,|,sub,sup,|,charmap,emotions,iespell,media,advhr,|,print,|,ltr,rtl,|,fullscreen",
        theme_advanced_buttons4: "insertlayer,moveforward,movebackward,absolute,|,styleprops,spellchecker,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,template,blockquote,pagebreak,|,insertfile,insertimage",
        theme_advanced_toolbar_location: "top",
        theme_advanced_toolbar_align: "left",
        theme_advanced_statusbar_location: "bottom",
        theme_advanced_resizing: true,
        setup: function (ed) {
            // Add a custom button
            ed.addButton('addImg', {
                title: 'Insert Image',
                image: '../assets/tinymce3.x/jscripts/tiny_mce/themes/advanced/img/insert-image3.png',
                onclick: function () {
                    // Add you own code to execute something on click
                    //$("#btnAddImage").click();
                    OpenPopupWindow("../admins/SelectImage.aspx?c=hfImgPath", 800, 600);
                }
            });
            ed.onInit.add(function(ed, evt) { 
                var dom = ed.dom; 
                var doc = ed.getDoc(); 
                tinymce.dom.Event.add(doc.body, 'blur', function (e) {
                    ed.save();
                });
            });
        },
        // Skin options
        skin: "o2k7",
        skin_variant: "silver",

        // Example content CSS (should be your site CSS)
        //content_css: "css/example.css",

        // Drop lists for link/image/media/template dialogs
        template_external_list_url: "js/template_list.js",
        external_link_list_url: "js/link_list.js",
        external_image_list_url: "js/image_list.js",
        media_external_list_url: "js/media_list.js",
    });
}