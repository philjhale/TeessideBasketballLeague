$().ready(function () {
    setMailTo();
    setExternalLinks();
    setClickOnce();

    // Fix for stupid Chrome date issue
    // http: //stackoverflow.com/a/7976335/299048
    jQuery.validator.methods["date"] =
        function (value, element) {
            var check = false;
            var re = /^\d{1,2}\/\d{1,2}\/\d{4}$/;
            if (re.test(value)) {
                var adata = value.split('/');
                var gg = parseInt(adata[0], 10);
                var mm = parseInt(adata[1], 10);
                var aaaa = parseInt(adata[2], 10);
                var xdata = new Date(aaaa, mm - 1, gg);
                if ((xdata.getFullYear() == aaaa)
                       && (xdata.getMonth() == mm - 1)
                       && (xdata.getDate() == gg))
                    check = true;
                else
                    check = false;
            } else
                check = false;
            return this.optional(element) || check;
        };
});


/**
* This method provides way of protecting email addresses
* from spammers. It searches for links with a specified 
* class and replaces the href attribute with the rel
* attribute (more or less). To function correctly the
* email address must be in the following format:
* <a href="#" rel="me/example.com" class="email"></a>
*/
function setMailTo() {
    $('a.email').each(function() {
        e = this.rel.replace('/', '@');
        this.href = 'mailto:' + e;
        
        if ($(this).text() == "")
            $(this).text(e);
    });
}

/**
* Forces all links with rel="external" attribute to
* open in a new window. This allows links to open in
* a new window and still comply with the XHTML strict
* schema
*/
function setExternalLinks() {
    $("a[rel=external]").click(function(){
      this.target = "_blank";
    });
}


/*
* Finds all buttons with the clickOnce attribute and adds a click event 
* which hides the button add adds another disabled button in it's place.
* This happens because if you disable the button in IE8 (and possibly 
* other versions) it doesn't submit the form
*/
function setClickOnce() {
    $("form").submit(function () {
        $(this).find("input.clickOnce").attr("disabled", "disabled");
    });
//    $("input.clickOnce").click(function () {
//        $(this).hide();
//        $(this).after('<input type="button" disabled="disabled" value="' + $(this).val() + '" />');
//        return true;
//    });
}

