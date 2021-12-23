using System;
using KompasConnector;
using BottleParameters;
using Kompas6API5;
using Kompas6Constants;
using Kompas6Constants3D;

namespace BottleBuilder
{
    public class Builder
    {
        private Konnector _connector;

        public void BuildBottle(Konnector konnector, Parameters parameters)
        {
            _connector = konnector;

            var coverRadius = parameters.FindParameter(ParameterTypeEnum.CoverRadius, 
                parameters.ParametersList);
            var handleBaseRadius = parameters.FindParameter(ParameterTypeEnum.HandleBaseRadius,
                parameters.ParametersList);
            var handleRadius = parameters.FindParameter(ParameterTypeEnum.HandleRadius,
                parameters.ParametersList);
            var handleLength = parameters.FindParameter(ParameterTypeEnum.HandleLength,
                parameters.ParametersList);
            var height = parameters.FindParameter(ParameterTypeEnum.Height,
                parameters.ParametersList);
            var width = parameters.FindParameter(ParameterTypeEnum.Width,
                parameters.ParametersList);
            var wallThickness = parameters.FindParameter(ParameterTypeEnum.WallThickness,
                parameters.ParametersList);

            double filletX = 0;
            double filletY = 0;
            double filletZ = 0;

            

            var sketch = CreateSketch(Obj3dType.o3d_planeXOY, true, 0, 0, 0);

            var doc2D = (ksDocument2D)sketch.BeginEdit();

            var circle = (ksCircleParam) _connector
                .Kompas
                .GetParamStruct((short) StructType2DEnum.ko_CircleParam);

            circle.xc = 0;
            circle.yc = 0;
            circle.style = 1;
            circle.rad = width / 2 - wallThickness;

            doc2D.ksCircle(circle.xc, circle.yc, circle.rad, circle.style);

            sketch.EndEdit();



            PressOutSketchThickness(sketch, height/4*3, wallThickness, true, 0);

            PressOutSketch(sketch, wallThickness, false, 0);

            filletX = width  / 2;
            CreateEdgeFillet(filletX, filletY, filletZ, wallThickness);
            
            sketch = CreateSketch(Obj3dType.o3d_sketch, false, width / 2 - 2, 0, height / 4 * 3);

            doc2D = sketch.BeginEdit();
            ksDocument2D iDocument2D = _connector.Kompas.ActiveDocument2D();
            doc2D.ksCircle(0, 0, width / 8 * 3, 1);
            doc2D.ksCircle(0, 0, width / 2, 1);
            sketch.EndEdit();

            PressOutSketch(sketch, wallThickness, false, 0);

            filletX = width / 2;
            filletY = 0;
            filletZ = height / 4 * 3 + wallThickness;

            CreateEdgeFillet(filletX, filletY, filletZ, wallThickness);

            

            sketch = CreateSketch(Obj3dType.o3d_sketch, false, width / 2 - wallThickness - 2, 0, height / 4 * 3 + wallThickness);
            doc2D = sketch.BeginEdit();
            doc2D.ksCircle(0, 0, width / 8 * 3, 1);
            sketch.EndEdit();
            var a = (coverRadius / 2 - width / 8 * 3 - wallThickness);
            var b = (height / 4);
            double angle = (Math.Atan((a / b)) /Math.PI*180);
            PressOutSketchThickness(sketch, height / 4, wallThickness, true, angle);
            CreateEdgeFillet(coverRadius/2, 0, height + wallThickness, wallThickness / 2);
            CreateEdgeFillet(coverRadius/2 - wallThickness, 0, height + wallThickness, wallThickness / 2);

            a = (coverRadius / 2 - width / 8 * 3 - wallThickness);
            b = (height / 4 + wallThickness);
            angle = Math.Atan(a / b) / Math.PI * 180;

            PressOutSketch(sketch, height/4 + wallThickness, true, angle);
            CreateChamfer(coverRadius / 2 - wallThickness, 0, height + wallThickness * 2, wallThickness, wallThickness);

            sketch = CreateSketch(Obj3dType.o3d_sketch, false, 0, 0, height + wallThickness*2);
            doc2D = sketch.BeginEdit();
            doc2D.ksCircle(0, 0, handleBaseRadius/2, 1);
            sketch.EndEdit();
            PressOutSketch(sketch, handleLength, true, 0);
            

            sketch = CreateSketch(Obj3dType.o3d_sketch, false, 0, 0, height + wallThickness*2 + handleBaseRadius);
            doc2D = sketch.BeginEdit();
            doc2D.ksCircle(0, 0, handleRadius / 2, 1);
            sketch.EndEdit();
            PressOutSketch(sketch, handleRadius, true, 0);
            
            CreateFaceFillet(handleRadius/2, 0, height + wallThickness*2 + handleLength + handleRadius/2, handleRadius / 2);

            CreateFaceFillet(handleBaseRadius / 2, 0, height + wallThickness*2 + handleLength/2, handleBaseRadius / 4);
            

            //BuildHandle( konnector,  parameters);
        }

        /// <summary>
        /// Создание эскиза
        /// </summary>
        /// <param name="planeType">Выбор плоскости</param>
        /// <returns>ksSketchDefinition</returns>
        private ksSketchDefinition CreateSketch(Obj3dType planeType, 
            bool isFirstSketch, double x, double y, double z)
        {
            ksEntity plane = (ksEntity)_connector
                .KsPart
                .GetDefaultEntity((short)planeType);

            var sketch = (ksEntity)_connector
                .KsPart
                .NewEntity((short)Obj3dType.o3d_sketch);

            var sketchDefinition = (ksSketchDefinition)sketch.GetDefinition();
            if (!isFirstSketch)
            {
                ksEntityCollection iCollection = 
                    _connector.KsPart.EntityCollection((short)Obj3dType.o3d_face);
                iCollection.SelectByPoint(x, y, z);
                plane = iCollection.First();
            }
            sketchDefinition.SetPlane(plane);

            sketch.Create();
            return sketchDefinition;
        }

        /// <summary>
        /// Выдавливание по эскизу
        /// </summary>
        /// <param name="sketchDefinition">Эскиз</param>
        /// <param name="thickness">Толщина</param>
        private void PressOutSketch(
            ksSketchDefinition sketchDefinition,
            double thickness, bool side, double draftValue)
        {
            var extrusionEntity = (ksEntity)_connector
                .KsPart
                .NewEntity((short)Obj3dType.o3d_bossExtrusion);

            var extrusionDefinition = (ksBossExtrusionDefinition)extrusionEntity
                .GetDefinition();
            
            extrusionDefinition.SetSideParam(side, 0, thickness);

            extrusionDefinition.SetSketch(sketchDefinition);
            ExtrusionParam extrusionParam =  extrusionDefinition.ExtrusionParam();
            extrusionParam.depthNormal = thickness;
            extrusionParam.draftValueNormal = draftValue;

            extrusionEntity.Create();
        }

        /// <summary>
        /// Выдавливание по эскизу
        /// </summary>
        /// <param name="sketchDefinition">Эскиз</param>
        /// <param name="thickness">Толщина</param>
        private void PressOutSketchThickness( ksSketchDefinition sketchDefinition,
            double height, double wallThickness, bool side, double draftValue)
        {

            var extrusionEntity = (ksEntity)_connector
                .KsPart
                .NewEntity((short)Obj3dType.o3d_baseExtrusion);
            
            var extrusionDefinition = (ksBaseExtrusionDefinition)extrusionEntity
                .GetDefinition();
            
            extrusionDefinition.SetSideParam(side, 0, height);
            extrusionDefinition.SetThinParam(true, 0, wallThickness);
            
            
            extrusionDefinition.SetSketch(sketchDefinition);
            ExtrusionParam extrusionParam = extrusionDefinition.ExtrusionParam();
            extrusionParam.draftValueNormal = draftValue;
            extrusionEntity.Create();
        }

        private ksEntity AddFillet(double radius)
        {
            ksEntity filletEntity = (ksEntity)_connector
                .KsPart
                .NewEntity((short)Obj3dType.o3d_fillet);

            var filletDefinition = (ksFilletDefinition)filletEntity.GetDefinition();

            filletDefinition.radius = radius;

            filletDefinition.tangent = true;
            
            return filletEntity;
        }

        private void CreateChamfer(double x, double y, double z, double distance1, double distance2)
        {
            ksEntity chamferEntity = (ksEntity)_connector
                .KsPart
                .NewEntity((short)Obj3dType.o3d_chamfer);

            var chamferDefinition = (ksChamferDefinition)chamferEntity.GetDefinition();
            chamferDefinition.SetChamferParam(true, distance1, distance2);

            ksEntityCollection iArray = chamferDefinition.array();
            ksEntityCollection iCollection = _connector.KsPart.EntityCollection((short)Obj3dType.o3d_edge);

            iCollection.SelectByPoint(x, y, z);

            var iEdge = iCollection.Last();
            iArray.Add(iEdge);

            chamferEntity.Create();
        }

        private void CreateFaceFillet(double x, double y, double z, double radius)
        {
            ksEntity filletEntity = AddFillet(radius);
            var filletDefinition = (ksFilletDefinition)filletEntity.GetDefinition();

            ksEntityCollection iArray = filletDefinition.array();
            ksEntityCollection iCollection = _connector.KsPart.EntityCollection((short)Obj3dType.o3d_face);

            iCollection.SelectByPoint(x, y, z);
            var iFace = iCollection.First();
            iArray.Add(iFace);

            filletEntity.Create();
        }

        private void CreateEdgeFillet(double x, double y, double z, double radius)
        {
            ksEntity filletEntity = AddFillet(radius);
            var filletDefinition = (ksFilletDefinition)filletEntity.GetDefinition();

            ksEntityCollection iArray = filletDefinition.array();
            ksEntityCollection iCollection = _connector.KsPart.EntityCollection((short)Obj3dType.o3d_edge);

            iCollection.SelectByPoint(x, y, z);
            var iEdge = iCollection.Last();
            iArray.Add(iEdge);

            filletEntity.Create();
        }

        private void BuildHandle(Konnector konnector, Parameters parameters)
        {
            var coverRadius = parameters.FindParameter(ParameterTypeEnum.CoverRadius,
                parameters.ParametersList);
            var handleBaseRadius = parameters.FindParameter(ParameterTypeEnum.HandleBaseRadius,
                parameters.ParametersList);
            var handleRadius = parameters.FindParameter(ParameterTypeEnum.HandleRadius,
                parameters.ParametersList);
            var handleLength = parameters.FindParameter(ParameterTypeEnum.HandleLength,
                parameters.ParametersList);
            var height = parameters.FindParameter(ParameterTypeEnum.Height,
                parameters.ParametersList);
            var width = parameters.FindParameter(ParameterTypeEnum.Width,
                parameters.ParametersList);
            var wallThickness = parameters.FindParameter(ParameterTypeEnum.WallThickness,
                parameters.ParametersList);

            var sketch = CreateSketch(Obj3dType.o3d_planeXOY, true, 0, 0, 0);

            var doc2D = (ksDocument2D)sketch.BeginEdit();

            var circle = (ksCircleParam)_connector
                .Kompas
                .GetParamStruct((short)StructType2DEnum.ko_CircleParam);

            double angle = -Math.Atan(Math.Tan(coverRadius / 2 - width / 8 * 3)) * 180 / Math.PI;

            sketch = CreateSketch(Obj3dType.o3d_sketch, false, width / 2 - 2, 0, height / 4 * 3 + wallThickness);
            doc2D = sketch.BeginEdit();
            doc2D.ksCircle(0, 0, width / 8 * 3, 1);
            sketch.EndEdit();

            PressOutSketch(sketch,  wallThickness, true, angle);
        }
    }
}
