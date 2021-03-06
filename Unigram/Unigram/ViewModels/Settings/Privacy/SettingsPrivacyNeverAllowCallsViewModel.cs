﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Td.Api;
using Unigram.Services;

namespace Unigram.ViewModels.Settings.Privacy
{
    public class SettingsPrivacyNeverAllowCallsViewModel : SettingsPrivacyNeverViewModelBase
    {
        public SettingsPrivacyNeverAllowCallsViewModel(IProtoService protoService, ICacheService cacheService, IEventAggregator aggregator)
            : base(protoService, cacheService, aggregator, new UserPrivacySettingAllowCalls())
        {
        }

        public override string Title => Strings.Resources.NeverAllow;
    }
}
