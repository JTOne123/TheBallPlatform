(function(){dust.register("textcontent_detailpane.dust",body_0);function body_0(chk,ctx){return chk.write("<div class=\"header clearfix\"><h2 class=\"clearfix\">").reference(ctx.get("Title"),ctx,"h").write("<br>").exists(ctx.get("ImageData"),ctx,{"block":body_1},null).exists(ctx.get("SubTitle"),ctx,{"block":body_2},null).write("</h2><div class=\"border border-news clearfix\">").exists(ctx.get("Categories"),ctx,{"block":body_3},null).write("</div></div><div class=\"content\"><div class=\"multicolumnElement\" style=\"padding-left:15px;padding-right:15px;\">").reference(ctx.get("BodyRendered"),ctx,"h",["s"]).write("</div></div><div class=\"dusttest\">").exists(ctx.get("Categories"),ctx,{"block":body_5},null).write("</div><div id=\"OrderForm\" style=\"display: none\"><h3>Ilmoittaudu mukaan!</h3><label for=\"OrderEmailAddress\">Sähköpostiosoitteesi</label><input id=\"OrderEmailAddress\" type=\"text\"><a data-toggle=\"modal\" role=\"button\" href=\"#\" class=\"btn btn-primary\">Lähetä tilaus!</a></div><script>var ShowOrderForm = function() {$(\"#ShowOrderFormButton\").hide();$(\"#OrderForm\").show();}</script><div class=\"reading-pane-footer\"><p></p><p><i class=\"icon-user\"></i>  <a href=\"#\">").exists(ctx.get("Author"),ctx,{"block":body_8},null).write("</a>| <i class=\"icon-calendar\"></i>| <i class=\"icon-bookmark\"></i> <input class=\"text input-xxlarge \" name=\"share_url\" value=\"https://schools.caloom.com/html/index.html?type=textcontent&id=").reference(ctx.get("ID"),ctx,"h").write("\"></p></div>");}function body_1(chk,ctx){return chk.write("<img src=\"../../AaltoGlobalImpact.OIP/MediaContent/").reference(ctx.getPath(false,["ImageData","ID"]),ctx,"h").write("_320x240_crop").reference(ctx.getPath(false,["ImageData","AdditionalFormatFileExt"]),ctx,"h").write("\" />");}function body_2(chk,ctx){return chk.write("<small>").reference(ctx.get("SubTitle"),ctx,"h").write("</small>");}function body_3(chk,ctx){return chk.write("<span class=\"label label-news\">").section(ctx.getPath(false,["Categories","CollectionContent"]),ctx,{"block":body_4},null).write("</span>");}function body_4(chk,ctx){return chk.reference(ctx.get("Title"),ctx,"h");}function body_5(chk,ctx){return chk.section(ctx.getPath(false,["Categories","CollectionContent"]),ctx,{"block":body_6},null);}function body_6(chk,ctx){return chk.helper("eq",ctx,{"block":body_7},{"key":ctx.get("CategoryName"),"value":"catTuleOppimaan"});}function body_7(chk,ctx){return chk.write("<a id=\"ShowOrderFormButton\" data-toggle=\"modal\" role=\"button\" href=\"javascript:ShowOrderForm();\" class=\"btn btn-primary\">Tule mukaan oppimaan!</a>");}function body_8(chk,ctx){return chk.reference(ctx.get("Author"),ctx,"h");}return body_0;})();