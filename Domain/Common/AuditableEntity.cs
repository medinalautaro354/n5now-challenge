using System;

namespace N5NowApi.Domain.Common;

public record struct AuditableEntity(DateTime Created, string CreatedBy, DateTime? LastModified, string LastModifiedBy) { }