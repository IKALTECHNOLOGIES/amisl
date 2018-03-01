<%@ Page Title="Niveles" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Nivel.aspx.cs" Inherits="ONG._Nivel" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    

    <br />
    <br />

    

   <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
       <link href="JSTree/themes/default/style.min.css" rel="stylesheet" />

<head>
    <title></title>
    
    
</head>
<body>
				<div class="row">
					<div class="col-md-4 col-sm-8 col-xs-8">
						<button type="button" class="btn btn-success btn-sm" onclick="Tree_create();"><i class="glyphicon glyphicon-asterisk"></i> Create</button>
						<button type="button" class="btn btn-warning btn-sm" onclick="Tree_rename();"><i class="glyphicon glyphicon-pencil"></i> Rename</button>
						<button type="button" class="btn btn-danger btn-sm" onclick ="Tree_delete();"><i class="glyphicon glyphicon-remove"></i> Delete</button>
					</div>
					
				</div>


    <div id="ajaxTree">    </div>

        <script src="Scripts/jquery-1.10.2.min.js"></script>
       <script src="JSTree/jstree.js"></script>

      <script>

          function guid() {
              function s4() {
                  return Math.floor((1 + Math.random()) * 0x10000)
                    .toString(16)
                    .substring(1);
              }
              return s4() + s4() + '' + s4() + '' + s4() + '' +
                s4() + '' + s4() + s4() + s4();
          }

          function Tree_create() {
              var ref = $('#ajaxTree').jstree(true),
                  sel = ref.get_selected();
              if (!sel.length) { return false; }
              sel = sel[0];
              sel = ref.create_node(sel, { "type": "file", "id": guid() });

              if (sel) {
                  ref.edit(sel, "", endCreate);
              }

          };
          function endCreate(node) {
              $.ajax({ url: "Utilitarios/TreeDataNivel.ashx?op=C", data: node })
          };
          function renameAjax(node) {
              $.ajax({ url: "Utilitarios/TreeDataNivel.ashx?op=M", data: node })
          };
          function Tree_rename() {
              var ref = $('#ajaxTree').jstree(true),
                  sel = ref.get_selected();
              if (!sel.length) { return false; }
              sel = sel[0];
              ref.edit(sel, "", renameAjax);
          };

          function Tree_delete() {
              var ref = $('#ajaxTree').jstree(true),
                  sel = ref.get_selected();
              if (!sel.length) { return false; }
              removeAjax(sel);
              ref.delete_node(sel);
          };
          function removeAjax(node) {
              $.ajax({ url: "Utilitarios/TreeDataNivel.ashx?op=B", data: "UUID=" + node[0] })
          };

          $(function () {
              // 6 create an instance when the DOM is ready
              $('#ajaxTree').jstree({
                  "types": {
                      "default": {
                          "icon": "glyphicon glyphicon-flash"
                      },
                      "root": {
                          "icon": "glyphicon glyphicon-ok"
                      }
                  },
                  'plugins': ["dnd", "search", "state", "types", "wholerow"],
                  'core': {
                      "animation": 0,

                      "themes": {
                          "stripes": true
                      },
                      "check_callback": true,
                      'force_text': true,
                      'data': {
                          "url": "Utilitarios/TreeDataNivel.ashx?op=V",
                          "dataType": "json"
                      },

                  }
              });
              // 7 bind to events triggered on the tree
              $('#ajaxTree').on("changed.jstree", function (e, data) {
                  console.log(data.selected);
              });

          });
      </script>

</body>
</html>
    
    
</asp:Content>

