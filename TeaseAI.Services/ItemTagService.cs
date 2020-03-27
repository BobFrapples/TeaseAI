using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeaseAI.Common.Constants;
using TeaseAI.Common.Data;
using TeaseAI.Common.Interfaces;
using TeaseAI.Data.Interfaces;

namespace TeaseAI.Services
{
    public class ItemTagService : IItemTagService
    {
        private readonly IItemTagRepository _itemTagRepository;

        public ItemTagService(IItemTagRepository itemTagRepository)
        {
            _itemTagRepository = itemTagRepository;
        }

        public List<ItemTag> Get()
        {
            return _itemTagRepository.Get();
        }

        public void Initialize()
        {
            var defaultTags = CreateDefaultTags();
            foreach (var tag in defaultTags)
            {
                var getTag = _itemTagRepository.Get(tag.Id);
                if (getTag.IsFailure)
                    _itemTagRepository.Create(tag);
                else
                    _itemTagRepository.Update(tag);
            }
        }

        private List<ItemTag> CreateDefaultTags()
        {
            var tagIds = (ItemTagId[])Enum.GetValues(typeof(ItemTagId));

            var defaultTagList = new List<ItemTag>();
            foreach (var tagId in tagIds)
            {
                defaultTagList.Add(
                    new ItemTag
                    {
                        Id = (int)tagId,
                        Name = tagId.ToString(),
                        Description = string.Empty,
                    });
            }
            defaultTagList.Single(it => it.Id == (int)ItemTagId.Hardcore).Description = "Shows EVERYTHING in graphic detail";
            defaultTagList.Single(it => it.Id == (int)ItemTagId.Lesbian).Description = "Girls loving girls";
            defaultTagList.Single(it => it.Id == (int)ItemTagId.Gay).Description = "Boys loving boys";
            defaultTagList.Single(it => it.Id == (int)ItemTagId.Bisexual).Description = "Loving everybody";

            return defaultTagList;
        }
    }
}
