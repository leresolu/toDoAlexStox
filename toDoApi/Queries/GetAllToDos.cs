using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using MediatR;
using toDoApi.Models;

namespace toDoApi.Queries
{
    public class GetAllToDos: IRequest<List<ToDo>> { }
    public class GetAllToDosHandler: IRequestHandler<GetAllToDos, List<ToDo>>
    {
        private readonly FirestoreDb _firestoreDb;
        private const string _collectionName = "todos";
        public GetAllToDosHandler(IFirestoreService firestore)
        {
            _firestoreDb = firestore.FirestoreDb;
        }
        public async Task<List<ToDo>> Handle (GetAllToDos request, CancellationToken cancellationToken)
        {
            var collection = _firestoreDb.Collection(_collectionName);
            var snapshot = await collection.GetSnapshotAsync();

            var toDos = snapshot.Documents.Select(s => s.ConvertTo<ToDo>()).ToList();
            return toDos.ToList();
        }
    }
}
