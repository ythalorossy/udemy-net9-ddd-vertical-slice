// Thrid-party libraries
// Internal libraries
global using Articles.Abstractions;
global using Articles.Abstractions.Enums;
global using FluentValidation;
global using MediatR;
// Application
global using Submission.Application.Features.Shared;
// Domain
global using Submission.Domain.Entities;
global using Submission.Domain.Enums;
// Persistance
global using Submission.Persistence.Repositories;
global using AssetTypeDefinitionRepository = Blocks.EntityFramework.CachedRepository<
    Submission.Persistence.SubmissionDbContext,
    Submission.Domain.Entities.AssetTypeDefinition,
    Articles.Abstractions.Enums.AssetType>;