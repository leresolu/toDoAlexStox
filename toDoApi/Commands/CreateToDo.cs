using Google.Cloud.Firestore;
using MediatR;
using toDoApi.Models;

namespace toDoApi.Commands
{
    public class CreateToDo: IRequest<ToDo>
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public Priorities? Priority { get; set; }
        public DateTime? DueDate { get; set; }
    }

    public class CreateToDoHandler: IRequestHandler<CreateToDo, ToDo>
    {
        private readonly FirestoreDb _firestoreDb;
        private const string _collectionName = "todos";
        public CreateToDoHandler(IFirestoreService firestore)
        {
            _firestoreDb = firestore.FirestoreDb;
        }
        public async Task<ToDo> Handle(CreateToDo request, CancellationToken cancellationToken)
        {

            // validate logic
            // update data store
            // update cache
            var toDo = new ToDo
            {
                Id = request.Id.ToString(),
                OwnerId = request.OwnerId.ToString(),
                Content = request.Content ?? "",
                CreatedDate = DateTime.UtcNow,
                DueDate = request.DueDate,
                Title = request.Title ?? "",
                Priority = request.Priority ?? Priorities.None
            };
            var collection = _firestoreDb.Collection(_collectionName);
            var document = collection.Document(request.Id.ToString());
            await document.CreateAsync(toDo);

            return toDo;
        }
    }
}
