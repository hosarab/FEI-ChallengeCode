using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using AutoMapper;
using Insight.Application.Mappings;
using Insight.Application.PostCodesFeatures.Queries;
using Insight.Domain.Entities;
using Xunit;

namespace Insight.IntegrationTest.Mapping
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<GeneralProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Theory]
        [InlineData(typeof(PostCode), typeof(PostcodeDetailsViewModel))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = Activator.CreateInstance(source);

            _mapper.Map(instance, source, destination);
        }


        [Theory]
        [InlineData(typeof(List<PostCode>), typeof(IEnumerable<PostcodeDetailsViewModel>))]
        public void ShouldSupportMappingFromSourceToDestinationAllPostcode(Type source, Type destination)
        {
            //var instance = Activator.CreateInstance(source);
            var instance = GetInstanceOf(source);

            _mapper.Map(instance, source, destination);
        }


        private static object GetInstanceOf(Type type)
        {
            if (type.GetConstructor(Type.EmptyTypes) != null)
                return Activator.CreateInstance(type);

            // Type without parameterless constructor
            return FormatterServices.GetUninitializedObject(type);
        }
    }
}
