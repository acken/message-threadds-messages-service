using UniscaleDemo.Messages;
using Uniscale.Core;
using Uniscale.Core.Session;
using Uniscale.Core.Session.Platform;

namespace MessagesService.Messages {
    public class InterceptorHandler {
        public void Setup(PlatformInterceptorBuilder builder,ForwardingSession forwardSession) {
            builder.InterceptMessage(Patterns.Messages.SendDirectMessage.AllMessageUsages,Patterns.Messages.SendDirectMessage.Handle(SendDirectMessageHandler));
            builder.InterceptRequest(Patterns.Messages.GetMessageList.AllRequestUsages,Patterns.Messages.GetMessageList.Handle(GetMessageListHandler));
            builder.InterceptMessage(Patterns.Messages.WelcomeUser.AllMessageUsages,Patterns.Messages.WelcomeUser.Handle(WelcomeUserHandler));
            builder.InterceptMessage(Patterns.Messages.SendMessage.AllMessageUsages,Patterns.Messages.SendMessage.Handle(SendMessageHandler));
            builder.InterceptMessage(Patterns.Messages.ReplyToDirectMessage.AllMessageUsages,Patterns.Messages.ReplyToDirectMessage.Handle(ReplyToDirectMessageHandler));
            builder.InterceptRequest(Patterns.Messages.GetDirectMessageList.AllRequestUsages,Patterns.Messages.GetDirectMessageList.Handle(GetDirectMessageListHandler));
        }
        
        public Task<Result> SendDirectMessageHandler(UniscaleDemo.Messages.Messages.SendDirectMessageInput input,FeatureContext ctx) {
            // Using the Uniscale SDK to implement a request interceptor handler In Uniscale the Result object is
            // used to return responses. For a successful response the Ok method is used. For validation errors
            // the Result.BadRequest method is used and for other errors the Result.InternalServerError method is
            // used
            // 
            // The endpoint functionality to implement
            // Sending a new direct message
            //   
            //   The acceptance criteria defined for the flow is:
            //     You must specify the user identifier of the sender of the message
            //     The message must be between 3 and 60 characters long
            //     The user identifier of the user you want to send the message to must be specified
            //   
            //   The following existing class is used for input:
            //   UniscaleDemo.Messages.Messages.SendDirectMessageInput
            //     # A reference to a user from the user service
            //     Receiver: System.Guid
            //     # Represents the message content in both timeline and direct messages
            //     Message: string
            //     # A reference to a user from the user service
            //     By: System.Guid
            // 
            // The available error codes to return are:
            //   ErrorCodes.Messages.ValidationError
            //   ErrorCodes.Messages.InvalidMessageLength
            return Task.FromResult(Result.Ok());
        }
        
        public Task<Result<List<UniscaleDemo.Messages.Messages.MessageFull>>> GetMessageListHandler(UniscaleDemo.Messages.Messages.Empty input,FeatureContext ctx) {
            // Using the Uniscale SDK to implement a request interceptor handler In Uniscale the
            // Result<UniscaleDemo.Messages.Messages.MessageFull> object is used to return responses. For a
            // successful response the Ok method is used. For validation errors the
            // Result<UniscaleDemo.Messages.Messages.MessageFull>.BadRequest method is used and for other errors
            // the Result<UniscaleDemo.Messages.Messages.MessageFull>.InternalServerError method is used
            // 
            // The endpoint functionality to implement
            // List messages
            //   
            //   The acceptance criteria defined for the flow is: The message list should fetch the latest 50
            //   messages from all users.
            //   
            //   The following existing class is used for input:
            //   UniscaleDemo.Messages.Messages.Empty
            //   
            //   The following existing class is used for output:
            //   UniscaleDemo.Messages.Messages.MessageFull
            //     Created: UniscaleDemo.Messages.Messages.UserTag
            //       At: System.DateTime
            //       # A reference to a user from the user service
            //       By: System.Guid
            //     MessageIdentifier: System.Guid
            //     # Represents the message content in both timeline and direct messages
            //     Message: string
            return Task.FromResult(Result.Ok(new List<UniscaleDemo.Messages.Messages.MessageFull>(new UniscaleDemo.Messages.Messages.MessageFull[] { 
                UniscaleDemo.Messages.Messages.MessageFull.Samples().DefaultSample()
            })));
        }
        
        public Task<Result> WelcomeUserHandler(UniscaleDemo.Messages.Messages.WelcomeUserInput input,FeatureContext ctx) {
            // Using the Uniscale SDK to implement a request interceptor handler In Uniscale the Result object is
            // used to return responses. For a successful response the Ok method is used. For validation errors
            // the Result.BadRequest method is used and for other errors the Result.InternalServerError method is
            // used
            // 
            // The endpoint functionality to implement
            // Welcome message
            //   When a user joins the thread a message should be added to the stream welcoming the user.
            //   
            //   The following existing class is used for input:
            //   UniscaleDemo.Messages.Messages.WelcomeUserInput
            //     WelcomedUser: UniscaleDemo.Messages.Messages.UserTag
            //       At: System.DateTime
            //       # A reference to a user from the user service
            //       By: System.Guid
            //     # Represents the message content in both timeline and direct messages
            //     Message: string
            return Task.FromResult(Result.Ok());
        }
        
        public Task<Result> SendMessageHandler(UniscaleDemo.Messages.Messages.SendMessageInput input,FeatureContext ctx) {
            // Using the Uniscale SDK to implement a request interceptor handler In Uniscale the Result object is
            // used to return responses. For a successful response the Ok method is used. For validation errors
            // the Result.BadRequest method is used and for other errors the Result.InternalServerError method is
            // used
            // 
            // The endpoint functionality to implement
            // Send message
            //   
            //   The acceptance criteria defined for the flow is:
            //     When sending a message the message body must be between 3 and 60 characters
            //     When sending a message the user identifier must be specified
            //   
            //   The following existing class is used for input:
            //   UniscaleDemo.Messages.Messages.SendMessageInput
            //     # A reference to a user from the user service
            //     By: System.Guid
            //     # Represents the message content in both timeline and direct messages
            //     Message: string
            // 
            // The available error codes to return are:
            //   ErrorCodes.Messages.ValidationError
            //   ErrorCodes.Messages.InvalidMessageLength
            return Task.FromResult(Result.Ok());
        }
        
        public Task<Result> ReplyToDirectMessageHandler(UniscaleDemo.Messages.Messages.ReplyToDirectMessageInput input,FeatureContext ctx) {
            // Using the Uniscale SDK to implement a request interceptor handler In Uniscale the Result object is
            // used to return responses. For a successful response the Ok method is used. For validation errors
            // the Result.BadRequest method is used and for other errors the Result.InternalServerError method is
            // used
            // 
            // The endpoint functionality to implement
            // Replying to a direct message
            //   
            //   The acceptance criteria defined for the flow is:
            //     The source direct message you are replying to must be specified
            //     The message must be between 3 and 60 characters long
            //     You must specify the user identifier of the sender of the message
            //   
            //   The following existing class is used for input:
            //   UniscaleDemo.Messages.Messages.ReplyToDirectMessageInput
            //     # Represents the message content in both timeline and direct messages
            //     Message: string
            //     # A reference to a user from the user service
            //     By: System.Guid
            //     DirectMessageIdentifier: System.Guid
            // 
            // The available error codes to return are:
            //   ErrorCodes.Messages.ValidationError
            //   ErrorCodes.Messages.InvalidMessageLength
            return Task.FromResult(Result.Ok());
        }
        
        public Task<Result<System.Collections.Generic.List<UniscaleDemo.Messages.Messages.DirectMessageFull>>> GetDirectMessageListHandler(System.Guid input,FeatureContext ctx) {
            // Using the Uniscale SDK to implement a request interceptor handler In Uniscale the
            // Result<UniscaleDemo.Messages.Messages.DirectMessageFull> object is used to return responses. For a
            // successful response the Ok method is used. For validation errors the
            // Result<UniscaleDemo.Messages.Messages.DirectMessageFull>.BadRequest method is used and for other
            // errors the Result<UniscaleDemo.Messages.Messages.DirectMessageFull>.InternalServerError method is
            // used
            // 
            // The endpoint functionality to implement
            // List direct messages
            //   
            //   The acceptance criteria defined for the flow is:
            //     You need to get the list of direct message for a user. You will then get both direct messages sent
            //     to you and sent from you
            //     Each direct message should contain a sorted list of replies where the oldest reply has the lowest
            //     sort number
            //   
            //   # A reference to a user from the user service
            //   The following existing class is used for input:: System.Guid
            //   
            //   The following existing class is used for output:
            //   UniscaleDemo.Messages.Messages.DirectMessageFull
            //     # A reference to a user from the user service
            //     Receiver: System.Guid
            //     Created: UniscaleDemo.Messages.Messages.UserTag
            //       At: System.DateTime
            //       # A reference to a user from the user service
            //       By: System.Guid
            //     # Represents the message content in both timeline and direct messages
            //     Message: string
            //     Replies: UniscaleDemo.Messages.Messages.DirectMessage_Replies[]
            //       Number: int
            //       # Represents the message content in both timeline and direct messages
            //       Message: string
            //       Created: UniscaleDemo.Messages.Messages.UserTag
            //         At: System.DateTime
            //         # A reference to a user from the user service
            //         By: System.Guid
            //     DirectMessageIdentifier: System.Guid
            return Task.FromResult(Result.Ok(new List<UniscaleDemo.Messages.Messages.DirectMessageFull>(new UniscaleDemo.Messages.Messages.DirectMessageFull[] { 
                UniscaleDemo.Messages.Messages.DirectMessageFull.Samples().DefaultSample()
            })));
        }
    }
}