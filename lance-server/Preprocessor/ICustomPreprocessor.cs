﻿using LanceServer.Core.Workspace;

namespace LanceServer.Preprocessor;

public interface ICustomPreprocessor
{
    string Filter(ReadDocument document);
}