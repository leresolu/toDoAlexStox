using Google.Cloud.Firestore;
using MediatR;
using toDoApi.Models;

namespace toDoApi.Commands
{
    public class UpdateToDo: IRequest<ToDo>
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public Priorities? Priority { get; set; }
        public DateTime? DueDate { get; set; }
    }

    public class UpdateToDoHandler: IRequestHandler<UpdateToDo, ToDo>
    {
        private readonly FirestoreDb _firestoreDb;
        private const string _collectionName = "todos";
        public UpdateToDoHandler(IFirestoreService firestore)
        {
            _firestoreDb = firestore.FirestoreDb;
        }
        public async Task<ToDo> Handle (UpdateToDo request, CancellationToken cancellationToken)
        {
            // validate logic
            // update data store
            // update cache
            var document = _firestoreDb.Collection(_collectionName).Document(request.Id.ToString());
            var snapshot = await document.GetSnapshotAsync(cancellationToken);
            var toDo = snapshot.ConvertTo<ToDo>();
            toDo.Title = request.Title ?? toDo.Title;
            toDo.Content = request.Content ?? toDo.Content;
            toDo.DueDate = request.DueDate ?? toDo.DueDate;
            toDo.Priority = request.Priority ?? toDo.Priority;
            await document.SetAsync(toDo);
            return toDo;
        }
    }
}
