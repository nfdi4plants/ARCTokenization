import { equal } from 'assert';
import { BuildingBlockType, BuildingBlockType__get_IsOutputColumn } from "./fable/src/ArcGraphModel/ArcType.js";

describe('Mocha native', function () {
    describe('subtestlist', function () {
        it('should return -1 when the value is not present', function () {
            equal([1, 2, 3].indexOf(4), -1);
        });
        it('test actual ArcGraphModel functions', function () {
            const ArcType_bbt_Source = new BuildingBlockType(4, []);
            equal(BuildingBlockType__get_IsOutputColumn(ArcType_bbt_Source),false)
        });
    });
});