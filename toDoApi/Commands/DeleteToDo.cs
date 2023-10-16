using Google.Cloud.Firestore;
using MediatR;
using toDoApi.Models;

namespace toDoApi.Commands
{
    public class DeleteToDo: IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteToDoHandler : IRequestHandler<DeleteToDo>
    {
        private readonly FirestoreDb _firestoreDb;
        private const string _collectionName = "todos";
        public DeleteToDoHandler(IFirestoreService firestore)
        {
            _firestoreDb = firestore.FirestoreDb;
        }
        public async Task Handle(DeleteToDo request, CancellationToken cancellationToken)
        {
            // validate logic
            // update data store
            // update cache
            await _firestoreDb.Collection(_collectionName).Document(request.Id.ToString()).DeleteAsync();
        }
    }
}
