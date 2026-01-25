// Thrid-party libraries
global using MediatR;
global using FluentValidation;

// Internal libraries
global using Articles.Abstractions;
global using Articles.Abstractions.Enums;

// Domain
global using Submission.Domain.Entities;
global using Submission.Domain.Enums;

// Application
global using Submission.Application.Features.Shared;

// Persistance
global using Submission.Persistence.Repositories;

global using AssetTypeDefinitionRepository = Blocks.EntityFramework.CachedRepository<
    Submission.Persistence.SubmissionDbContext,
    Submission.Domain.Entities.AssetTypeDefinition,
    Articles.Abstractions.Enums.AssetType>;