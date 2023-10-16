using Google.Cloud.Firestore;

namespace toDoApi.Models
{
    [FirestoreData]
    public class ToDo
    {
        [FirestoreDocumentId] public string Id { get; set; }
        [FirestoreProperty] public string? OwnerId { get; set; }
        [FirestoreProperty] public string? Title { get; set; }
        [FirestoreProperty] public string? Content { get; set; }
        [FirestoreProperty] public Priorities? Priority { get; set; }
        [FirestoreProperty] public DateTime CreatedDate { get; set; }
        [FirestoreProperty] public DateTime? DueDate { get; set; }
        [FirestoreProperty] public int ModelVersion { get; } = 1;
    }
}
