﻿using System;
using System.Collections.Generic;
using Castle.Components.DictionaryAdapter;
using BukStore.AbpZeroTemplate.Friendships.Dto;

namespace BukStore.AbpZeroTemplate.Chat.Dto
{
    public class GetUserChatFriendsWithSettingsOutput
    {
        public DateTime ServerTime { get; set; }
        
        public List<FriendDto> Friends { get; set; }

        public GetUserChatFriendsWithSettingsOutput()
        {
            Friends = new EditableList<FriendDto>();
        }
    }
}