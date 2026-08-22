using CSharpFunctionalExtensions;
using WebTickets.Domain.Shared;
using WebTickets.Domain.Users;

namespace WebTickets.Domain.Modules;

public class File : Shared.Entity<FileId>
{
    private File(FileId id) : base(id) { }

    private File(
        FileId id,
        string fileName,
        string storageKey,
        string contentType,
        long sizeBytes,
        UserId uploadedByUserId,
        TicketId? ticketId,
        MessageId? messageId) : base(id)
    {
        FileName = fileName;
        StorageKey = storageKey;
        ContentType = contentType;
        SizeBytes = sizeBytes;
        UploadedByUserId = uploadedByUserId;
        TicketId = ticketId;
        MessageId = messageId;
        UploadedAt = DateTime.UtcNow;
    }

    public string FileName { get; private set; } = null!;
    public string StorageKey { get; private set; } = null!; // ключ объекта в MinIO
    public string ContentType { get; private set; } = null!;
    public long SizeBytes { get; private set; }
    public UserId UploadedByUserId { get; private set; } = default!;
    public DateTime UploadedAt { get; private set; }

    public TicketId? TicketId { get; private set; }
    public virtual Ticket? Ticket { get; private set; }

    public MessageId? MessageId { get; private set; }
    public virtual Message? Message { get; private set; }

    public static Result<File> ForTicket(
        TicketId ticketId,
        string fileName,
        string storageKey,
        string contentType,
        long sizeBytes,
        UserId uploadedBy)
    {
        var validation = Validate(fileName, storageKey, sizeBytes);
        if (validation.IsFailure)
            return Result.Failure<File>(validation.Error);

        var file = new File(
            FileId.NewFileId(), fileName, storageKey, contentType, sizeBytes, uploadedBy, ticketId, null);

        return Result.Success(file);
    }

    public static Result<File> ForMessage(
        MessageId messageId,
        string fileName,
        string storageKey,
        string contentType,
        long sizeBytes,
        UserId uploadedBy)
    {
        var validation = Validate(fileName, storageKey, sizeBytes);
        if (validation.IsFailure)
            return Result.Failure<File>(validation.Error);

        var file = new File(
            FileId.NewFileId(), fileName, storageKey, contentType, sizeBytes, uploadedBy, null, messageId);

        return Result.Success(file);
    }

    private static Result Validate(string fileName, string storageKey, long sizeBytes)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return Result.Failure("FileName cannot be empty");

        if (string.IsNullOrWhiteSpace(storageKey))
            return Result.Failure("StorageKey cannot be empty");

        if (sizeBytes <= 0)
            return Result.Failure("SizeBytes must be positive");

        return Result.Success();
    }
}