<%@ Page Title="" Language="C#" MasterPageFile="~/CNNTwitterCrawler.Master" AutoEventWireup="true" CodeBehind="StartPage.aspx.cs" Inherits="CNNTwitterCrawler.ThePages.Start.StartPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<section class="content-header">
        <h1>Twitter Updates on Donald Trump 
        </h1>
    </section>--%>

    <section class="content">
              <form id="form1" runat="server"> 
              <div class="col-md-6">    

                  <br />
                  <br />
                  <br />
                  <br />
                  <br />
                  <br />
                  <br />
                  <br />
                  <br />
                  <br />

                  <button type="button" CssClass="next-btn" runat="server" OnServerClick="getCNNUpdates_OnServerClick" id="Submit1" class="btn btn-flat btn-primary pull-right"  style="background-color:crimson; font-family:'Comic Sans MS'">Get LatestCNN Updates about Donald Trump</button>           
                  
                  <br />
                  <br />
                  <br />
                  <br />

                  <button type="button" CssClass="next-btn" runat="server" OnServerClick="getTwitterUpdates_OnServerClick" id="Submit2" class="btn btn-flat btn-primary pull-right" style="font-family:'Comic Sans MS'">Get Latest Updates Twitter about Donald Trump</button> 

              </div>
              </form>
              
        
            </section>
</asp:Content>
