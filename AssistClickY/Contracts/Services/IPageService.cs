﻿using System.Windows.Controls;

namespace AssistClickY.Contracts.Services;

public interface IPageService
{
    Type GetPageType(string key);

    Page GetPage(string key);
}
