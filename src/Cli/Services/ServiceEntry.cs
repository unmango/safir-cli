using System;

namespace Cli.Services
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal record ServiceEntry
    {
        private string? _name;

        public string Name
        {
            get => _name ?? Enum.GetName(Service) ?? string.Empty;
            set => _name = value ?? throw new ArgumentNullException(nameof(Name));
        }
        
        public ServiceImplementation Service { get; init; }
        
        public ServiceSource Source { get; init; }
        
        public ServiceType Type { get; init; }
        
        public string? GitCloneUrl { get; init; }
        
        public string? ExtraArgs { get; init; }
        
        public string? Cwd { get; init; }
        
        public string? SourceDirectory { get; init; }
    }
}
