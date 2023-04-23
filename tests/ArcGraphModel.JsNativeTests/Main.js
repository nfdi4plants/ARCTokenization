import { equal } from 'assert';
import { BuildingBlockType$reflection, BuildingBlockType_tryOfString_Z721C83C5, BuildingBlockType__get_IsDeprecated, BuildingBlockType__get_IsFeaturedColumn, BuildingBlockType__get_IsTermColumn, BuildingBlockType__get_IsOutputColumn, BuildingBlockType__get_IsInputColumn, BuildingBlockType } from "./fable/src/ArcGraphModel/ArcType.js";

describe('Mocha native', function () {
    describe('subtestlist', function () {
        it('should return -1 when the value is not present', function () {
            equal([1, 2, 3].indexOf(4), -1);
        });
        it('test actual ArcGraphModel func', function () {
            const ArcType_bbt_Source = new BuildingBlockType(4, []);
            equal(BuildingBlockType__get_IsInputColumn(ArcType_bbt_Source),true)
        });
    });
});