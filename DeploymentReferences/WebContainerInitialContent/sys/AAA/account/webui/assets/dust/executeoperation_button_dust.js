(function(){dust.register("executeoperation_button.dust",body_0);function body_0(chk,ctx){return chk.write("<a data-toggle=\"modal\" role=\"button\" href=\"#\" class=\"open-").reference(ctx.get("form_name"),ctx,"h").write("\">").exists(ctx.get("icon_class_name"),ctx,{"block":body_1},null).exists(ctx.get("button_label"),ctx,{"block":body_2},null).write("</a>");}function body_1(chk,ctx){return chk.write("<i class=\"").reference(ctx.get("icon_class_name"),ctx,"h").write("\"></i>");}function body_2(chk,ctx){return chk.reference(ctx.get("button_label"),ctx,"h");}return body_0;})();