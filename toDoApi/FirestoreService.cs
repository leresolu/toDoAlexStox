using Google.Cloud.Firestore;

namespace toDoApi
{
    public interface IFirestoreService
    {
        FirestoreDb FirestoreDb { get; }
    }
    public class FirestoreService: IFirestoreService
    {
        private readonly FirestoreDb _db;
        public FirestoreService(FirestoreDb db)
        {
            _db = db;
        }

        public FirestoreDb FirestoreDb { get {  return _db; } }
    }
}
