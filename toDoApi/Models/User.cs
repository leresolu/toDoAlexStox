using Google.Cloud.Firestore;

namespace toDoApi.Models
{
    [FirestoreData]
    public class User
    {
        [FirestoreDocumentId] public string Id { get; set; }
        [FirestoreProperty] public string? Name { get; set; }
        [FirestoreProperty] public DateTime CreatedDate { get; set; }
        [FirestoreProperty] public int ModelVersion { get; } = 1;
    }
}
