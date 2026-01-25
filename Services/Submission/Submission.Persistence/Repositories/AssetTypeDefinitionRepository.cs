using Articles.Abstractions.Enums;
using Blocks.EntityFramework;
using Microsoft.Extensions.Caching.Memory;
using Submission.Domain.Entities;

namespace Submission.Persistence.Repositories;

//public class AssetTypeDefinitionRepository(SubmissionDbContext dbContext, IMemoryCache cache)
//    : CachedRepository<SubmissionDbContext, AssetTypeDefinition, AssetType>(dbContext, cache);