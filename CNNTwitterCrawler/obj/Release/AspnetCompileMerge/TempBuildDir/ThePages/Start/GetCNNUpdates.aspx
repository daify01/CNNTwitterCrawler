<%@ Page Title="" Language="C#" MasterPageFile="~/CNNTwitterCrawler.Master" AutoEventWireup="true" CodeBehind="GetCNNUpdates.aspx.cs" Inherits="CNNTwitterCrawler.ThePages.Start.GetCNNUpdates" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<section class="content-header">
        <h1>CNN Updates on Donald Trump 
        </h1>
    </section>--%>

    
          
          <div class="row">
            <div class="col-xs-12">
              <div class="box">
                  <div class="box-header" style="background-color:steelblue">
                  <h3 class="box-title" style="color:whitesmoke; font-family:Helvetica, Arial, sans-serif;">CNN Updates on Donald Trump </h3>
                </div><!-- /.box-header -->  
                   
                <div class="box-body">
                  <table id="viewupdates" class="table table-bordered table-striped">
                    <thead>
                      <tr>
                        <th></th>
                        <th>Headline</th>
                          <th></th>
                          <%--<th></th>--%>
                      </tr>
                    </thead>
                  </table>
                </div><!-- /.box-body -->
              </div><!-- /.box -->
                </div>
              </div>


     <div class="modal fade" id="ViewCNNUpdatesModal" tabindex="-1" role="dialog" aria-labelledby="ViewCNNUpdatesModal" aria-hidden="true">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="myModalLabel">CNN Article Feed</h4>
            </div>
            <div class="modal-body" id="detailsContent" style="overflow-y:scroll; max-height:1000px">
                
               
            </div>
            <%--<div class="modal-footer">
                <button type="button" id="Close" class="btn btn-default" data-dismiss="modal">Close</button>
                <button type="button" id="Edit" class="btn btn-primary">Edit Account</button>
            </div>--%>
        </div>
    </div>
</div>

    
        <section class="content">
              <form id="form1" runat="server"> 
              <div class="col-md-6">
                  <h3 class="box-title" style="color:black; font-family:Helvetica, Arial, sans-serif;">Share link From Gmail Account To Any Email </h3>
                  <label for="TextBoxNameEmail">Use this feature to share this link with people</label>
                  <b></b>
                  <br />
                            <div class="form-group">
                                <label for="TextBoxNameSenderEmailLabe">Your Email (gmail account)</label>
                        <input type="text" runat="server" class="form-control" id="TextBoxNameSenderEmail" title="Enter proper email format" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$" maxlength="100" required="" placeholder="Enter Your Email"/>
                             
                            </div>

                            <div class="form-group">
                   <label for="TextBoxNameSenderPassword">Your Email Password (It's needed just to send mail. We never store it)</label>
            <input type="password" runat="server" class="form-control" id="TextBoxNameSenderPassword" TextMode="Password" placeholder="Password" required=""/>
                    </div>

                        <div class="form-group">
                                <label for="TextBoxNameRecipientEmail">Recipient's Email (not necessarily gmail)</label>
                        <input type="text" runat="server" class="form-control" id="TextBoxNameRecipientEmail" title="Enter proper email format" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$" maxlength="100" required="" placeholder="Enter Recipient's Email"/>
                             
                            </div>
                            
                            <div class="form-group">
                                <label for="TextBoxNameMailSubject">Mail Subject</label>
                        <input type="text" runat="server" class="form-control" id="TextBoxNameMailSubject" title="Letters Only" pattern="[A-Z a-z]*" maxlength="200" required="" placeholder="Enter Mail Subject"/>
                               
                            </div>
                            
                  <div class="form-group">
                                <label for="TextAreaMailBody">Mail Body</label>
                        <textarea runat="server" class="form-control" id="TextAreaMailBody" cols="50" rows="10" required="required" placeholder="Enter Mail Body"/>                               
                            </div>
                        

                        <div class="box-footer">
            <%--<button type="button"  id="btnNext" CssClass="next-btn" runat="server" class="btn btn-flat btn-primary pull-right" OnServerClick="searchsubmit_OnServerClick">Send Mail</button>--%>
             <input type="submit" runat="server" OnServerClick="searchsubmit_OnServerClick" id="searchsubmit" class="btn btn-flat btn-primary pull-right" name="Add"/>
            </div> 
                                </div>
              </form>
              
        
            </section>

    <script type="text/javascript">
        $(function () {
            //debugger;
            var table = $("#viewupdates").dataTable({
                "serverSide": true,
                "ajax": {
                    "url": '/api/Query/TrumpCNNUpdatesForView', //url to fetch data from
                    "type": 'POST',
                    data: function (d) {
                    }
                },
                "searching": false,
                "fnRowCallback": function (nRow, aData, iDisplayIndex) {
                    var oSettings = table.fnSettings();
                    $("td:first", nRow).html(oSettings._iDisplayStart + iDisplayIndex + 1);
                    return nRow;
                },
                columns: [
                    { data: 'ID' },
                { data: 'HeadLine' },
                    { data: 'ViewDetails' },
                    //{ data: 'ShareOnFacebook' }
                ]
            });

            $("#searchsubmit").click(function () {
                table.fnFilter(''); //.search('').draw();
            });
        });

        function viewDetails(articleLink) {
            $('#ViewCNNUpdatesModal').modal('show');
            debugger;
            $.ajax({
                url: articleLink,
                method: 'POST',
                success: function (result) {
                    $("#detailsContent").html("<h2><i class='fa fa-thumbs-up'></i> Article has been opened in a new browser window</h2>");
                    window.open(articleLink);                    
                },
                error: function (xhr, status, error) {
                    $("#detailsContent").html("<h2><i class='fa fa-warning'></i> Couldn't fetch details, pls try again</h2>");
                }
            });
        }

        //$("#Edit").click(function () {
        //    location = '../ManageCustomerAcct/EditCustomerAccountReal.aspx?id=' + $(this).attr("data-id");
        //});

        function closeAccount(id) {
            alertify.confirm("Do you want to close this account?",
                function () {
                    $.ajax({
                        url: '/api/Query/CloseAccount?id=' + id,
                        method: 'POST',
                        success: function (result) {
                            debugger;
                            alertify.alert("This user's account has been closed");
                             $('.status-' + id).html('Closed').addClass('btn-danger').removeClass('btn-info');
                            //$("input[type=button]").attr('disabled', 'disabled');
                            //$("button[type=button]").removeAttr('disabled');
                             location.reload();
                        },
                        error: function () {
                            debugger;
                        }
                        //error: alertify.alert("This user's account could not be closed")
                    });
                });
        }

    </script>
</asp:Content>
