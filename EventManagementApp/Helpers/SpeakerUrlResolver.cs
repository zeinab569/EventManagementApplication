using AutoMapper;
using Core.Entities;
using EventManagementApp.Dtos.SpeakerDTOs;

namespace EventManagementApp.Helpers
{
    public class SpeakerUrlResolver : IValueResolver<Speaker, SpeakerDTO, string>
    {
        private readonly IConfiguration _config;

        public SpeakerUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Speaker source, SpeakerDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.SpeakerImage))
            {
                return _config["ApiUrl"] + source.SpeakerImage;
            }
            return null;
        }
    }
}
