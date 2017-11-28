using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Teams;
using Microsoft.Bot.Connector.Teams.Models;

namespace $safeprojectname$
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, () => new Dialogs.RootDialog());
            }
            else if (activity.Type == ActivityTypes.Invoke)
            {
                // ComposeExtension query
                if (activity.IsComposeExtensionQuery())
                {
                    // TODO: Handle your compose extension request
                    ComposeExtensionResponse invokeResponse = new ComposeExtensionResponse() { ComposeExtension = new ComposeExtensionResult() };
                    return Request.CreateResponse(HttpStatusCode.OK, invokeResponse);
                }
                //Actionable Message
                else if (activity.IsO365ConnectorCardActionQuery())
                {
                    // TODO: Handle your actionable message here
                    var connectorClient = new ConnectorClient(new Uri(activity.ServiceUrl));
                    O365ConnectorCardActionQuery o365CardQuery = activity.GetO365ConnectorCardActionQueryData();
                    Activity replyActivity = activity.CreateReply();
                    replyActivity.TextFormat = "xml";
                    replyActivity.Text = $@"
                        <h2>Thanks, {activity.From.Name}</h2><br/>
                        <h3>Your input action ID:</h3><br/>
                        <pre>{o365CardQuery.ActionId}</pre><br/>
                        <h3>Your input body:</h3><br/>
                        <pre>{o365CardQuery.Body}</pre>";
                    await connectorClient.Conversations.ReplyToActivityWithRetriesAsync(replyActivity);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                // PopUp SignIn
                else if (activity.Name == "signin/verifyState")
                {
                    // TODO: Handle your PopUp SignIn request 
                    var connectorClient = new ConnectorClient(new Uri(activity.ServiceUrl));
                    Activity replyActivity = activity.CreateReply();
                    replyActivity.Text = $@"Authentication Successful";
                    await connectorClient.Conversations.ReplyToActivityWithRetriesAsync(replyActivity);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                // Handle other invoke requests
                else
                {
                    // Parse the invoke value and change the message activity as well - useful if you've got action buttons which postback
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }            
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}