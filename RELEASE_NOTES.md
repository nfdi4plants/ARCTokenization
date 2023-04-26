### 0.1.0+4a89600 (Released 2023-4-25)
* Additions:
    * setup base types
    * setup base io functionality
    * [[#4a89600](https://github.com/nfdi4plants/https://github.com/nfdi4plants/commit/4a89600f5b7096bda85ed3658b005b4393e8e36c)] extend xlsx file tokenizer
    * [[#d83caf2](https://github.com/nfdi4plants/https://github.com/nfdi4plants/commit/d83caf2bf17c39b9b895622530f32669be210192)] add ArcGraphModel.IO keyparser tests
    * [[#868f54f](https://github.com/nfdi4plants/https://github.com/nfdi4plants/commit/868f54fc5778bfe7688829794ac1b584437e8e02)] add test script
    * [[#f94259f](https://github.com/nfdi4plants/https://github.com/nfdi4plants/commit/f94259fc3a9a876ff5a826628c367ddbefee0053)] move ArcGraphModel.IO to src folder
    * [[#5b27afe](https://github.com/nfdi4plants/https://github.com/nfdi4plants/commit/5b27afeb3843997dc54411f101097361e70923f5)] add token aggregation
    * [[#a0dc351](https://github.com/nfdi4plants/https://github.com/nfdi4plants/commit/a0dc3516859b1f466b4bbe0e2225599984cd0a2c)] add isa line tokenizer
    * [[#9f8e6d2](https://github.com/nfdi4plants/https://github.com/nfdi4plants/commit/9f8e6d2194cf2d0e9c68c04aa6de361bd8ce6fd7)] setup ArcGraphModel.IO project
    * [[#f8b82c2](https://github.com/nfdi4plants/https://github.com/nfdi4plants/commit/f8b82c242f61e56d906e89b3eda83a6d4de5a3f8)] change ci build target until fable compilation is stable
    * [[#1c9f557](https://github.com/nfdi4plants/https://github.com/nfdi4plants/commit/1c9f5579d639cef574cf39438c1d3e064569190c)] adjust tests for datamodel changes
    * [[#03ee664](https://github.com/nfdi4plants/https://github.com/nfdi4plants/commit/03ee664e84b15d6de84713124486aa4459e0372f)] add IAttributeCollection interface and additional helper functions
    * [[#6057a75](https://github.com/nfdi4plants/https://github.com/nfdi4plants/commit/6057a75018cb0879e14c64a0cf59ce1ffc4861a7)] Merge pull request #8 from Freymaurer/Fable
    * [[#013b381](https://github.com/nfdi4plants/https://github.com/nfdi4plants/commit/013b381f9b6751e79124c05261a713223c5ad916)] Merge pull request #9 from nfdi4plants/isaDataModel
    * [[#8c205a5](https://github.com/nfdi4plants/https://github.com/nfdi4plants/commit/8c205a5acabf94fc5ea2346ecec6b62968430adb)] adjust types and add helper methods
    * [[#f3386bd](https://github.com/nfdi4plants/https://github.com/nfdi4plants/commit/f3386bd558e6015247584cd1253e6c1ab0eb5653)] Start type remodeling
    * [[#81d1348](https://github.com/nfdi4plants/https://github.com/nfdi4plants/commit/81d1348d0cc413a433be67e14e42c369a898cc81)] Update tests
    * [[#18586b6](https://github.com/nfdi4plants/https://github.com/nfdi4plants/commit/18586b65248a28181be09a33e5ff25254e555f84)] update all tests to expecto :sparkles:
    * [[#b218731](https://github.com/nfdi4plants/https://github.com/nfdi4plants/commit/b218731bd95be43bed0aa4b8d0424bdf64781fc6)] Improve gitignore syntax
    * [[#5be34a5](https://github.com/nfdi4plants/https://github.com/nfdi4plants/commit/5be34a53e615d62505d73e2d1f7a08df739ebdde)] update README.md :books:
    * [[#16fef98](https://github.com/nfdi4plants/https://github.com/nfdi4plants/commit/16fef9839f1e339b34d792f0b0880a414c8f17e5)] setup new test suit
    * [[#c5790ee](https://github.com/nfdi4plants/https://github.com/nfdi4plants/commit/c5790ee1e288efe5a74569722b4e22d533e1fff6)] Ignore fable transpiled .js files
    * [[#1051961](https://github.com/nfdi4plants/https://github.com/nfdi4plants/commit/1051961ce6ca3e24fcd1bd9eb5959b42104ae979)] Update dependencies
    * [[#34edbe6](https://github.com/nfdi4plants/https://github.com/nfdi4plants/commit/34edbe6dcdd6817fa2f0bc7dd6dda0d0ba929df2)] Merge pull request #6 from nfdi4plants/AnnotationTableToGraph
    * [[#9b93e17](https://github.com/nfdi4plants/https://github.com/nfdi4plants/commit/9b93e1708acc1cadc38fc37894aea42c0ade132b)] Add another qualifier getting function
    * [[#35e7f7a](https://github.com/nfdi4plants/https://github.com/nfdi4plants/commit/35e7f7af38aba9b6c952052a7ebb34a6f97533c8)] Add functions to retrieve qualifier (value) from CvParam
    * [[#369e9f4](https://github.com/nfdi4plants/https://github.com/nfdi4plants/commit/369e9f429b86741766c2e5b7752e66de2ac66317)] Add CvContainer functions to get CvBase values
    * [[#ef9ee68](https://github.com/nfdi4plants/https://github.com/nfdi4plants/commit/ef9ee6888328ae45a9c9709010400ab306e96b91)] Try new functions in playground
    * [[#71b72de](https://github.com/nfdi4plants/https://github.com/nfdi4plants/commit/71b72de8181849965bff93beb4f94e82180c7c0d)] Add functions to return specific cell addresses
* Bugfixes:
    * [[#2808857](https://github.com/nfdi4plants/https://github.com/nfdi4plants/commit/28088574f4f1909025efedd5d0d7ec2dd7c91dd4)] fix investigation parser and add some addtional fields
    * [[#0784c70](https://github.com/nfdi4plants/https://github.com/nfdi4plants/commit/0784c702e52869a28d2e9719332b88867c37df9b)] Fix typo
    * [[#74e0bc9](https://github.com/nfdi4plants/https://github.com/nfdi4plants/commit/74e0bc968da1cd6393141da91ef8639dc5b5ddeb)] Downgrade fable dependency :bug:

### 0.0.0 (Released 2023-3-15)
* Additions:
    * Initial set up for RELEASE_Notes.md

