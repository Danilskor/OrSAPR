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

        private double _coverRadius; 

        private double _handleBaseRadius; 

        private double _handleRadius;

        private double _handleLength; 

        private double _height; 

        private double _width; 

        private double _wallThickness; 
         
        private double _filletX;

        private double _filletY;

        private double _filletZ;

        private double _filletRadius;
        
        private double _centreCircleX;

        private double _centreCircleY;

        private double _circleRadius;
        
        private double _sketchX;

        private double _sketchY;

        private double _sketchZ;

        ksSketchDefinition _sketch;

        public void BuildBottle(Konnector konnector, Parameters parameters)
        {
            _connector = konnector;
            _coverRadius = parameters.FindParameter(ParameterTypeEnum.CoverRadius,
            parameters.ParametersList); 
            _handleBaseRadius = parameters.FindParameter(ParameterTypeEnum.HandleBaseRadius,
            parameters.ParametersList);
            _handleRadius = parameters.FindParameter(ParameterTypeEnum.HandleRadius,
            parameters.ParametersList);
            _handleLength = parameters.FindParameter(ParameterTypeEnum.HandleLength,
            parameters.ParametersList);
            _height = parameters.FindParameter(ParameterTypeEnum.Height,
            parameters.ParametersList);
            _width = parameters.FindParameter(ParameterTypeEnum.Width,
            parameters.ParametersList);
            _wallThickness = parameters.FindParameter(ParameterTypeEnum.WallThickness,
            parameters.ParametersList);
            
            BuildBottleBase();
            BuildBottleTop();
            BuildHandle();
        }

        private void BuildHandle()
        {
            var angle = Math.Atan((_coverRadius / 2 - _width / 8 * 3 - _wallThickness) / (_height / 4 + _wallThickness)) / Math.PI * 180;
            PressOutSketch(_sketch, _height / 4 + _wallThickness, true, angle);
            CreateChamfer(_coverRadius / 2 - _wallThickness, 0, _height + _wallThickness * 2, _wallThickness, _wallThickness);

            _sketchX = 0;
            _sketchY = 0;
            _sketchZ = _height + _wallThickness * 2;
            _sketch = CreateSketch(Obj3dType.o3d_sketch, false, _sketchX, _sketchY, _sketchZ);

            _centreCircleX = 0;
            _centreCircleY = 0;
            _circleRadius = _handleBaseRadius / 2;
            CreateCircle(_sketch, _centreCircleX, _centreCircleY, _circleRadius);
            _sketch.EndEdit();

            PressOutSketch(_sketch, _handleLength, true, 0);

            _sketchX = 0;
            _sketchY = 0;
            _sketchZ = _height + _wallThickness * 2 + _handleLength;
            _sketch = CreateSketch(Obj3dType.o3d_sketch, false, _sketchX, _sketchY, _sketchZ);


            _centreCircleX = 0;
            _centreCircleY = 0;
            _circleRadius = _handleRadius / 2;
            CreateCircle(_sketch, _centreCircleX, _centreCircleY, _circleRadius);

            PressOutSketch(_sketch, _handleRadius, true, 0);

            _filletX = _handleRadius / 2;
            _filletY = 0;
            _filletZ = _height + _wallThickness * 2 + _handleLength + _handleRadius / 2;
            _filletRadius = _handleRadius / 2;
            CreateFaceFillet(_filletX, _filletY, _filletZ, _filletRadius);

            _filletX = _handleBaseRadius / 2;
            _filletY = 0;
            _filletZ = _height + _wallThickness * 2 + _handleLength / 2;
            _filletRadius = _handleBaseRadius / 4;
            CreateFaceFillet(_filletX, _filletY, _filletZ, _filletRadius);
        }


        private void BuildBottleBase()
        {
            _sketchX = 0;
            _sketchY = 0;
            _sketchZ = 0;
            _sketch = CreateSketch(Obj3dType.o3d_planeXOY, true, _sketchX, _sketchY, _sketchZ);

            _centreCircleX = 0;
            _centreCircleY = 0;
            _circleRadius = _width / 2 - _wallThickness;
            CreateCircle(_sketch, _centreCircleX, _centreCircleY, _circleRadius);


            PressOutSketchThickness(_sketch, _height / 4 * 3, _wallThickness, true, 0);

            PressOutSketch(_sketch, _wallThickness, false, 0);

            _filletX = _width / 2;
            _filletY = 0;
            _filletZ = 0;
            CreateEdgeFillet(_filletX, _filletY, _filletZ, _wallThickness);

            _sketchX = _width / 2 - 2;
            _sketchY = 0;
            _sketchZ = _height / 4 * 3;
            _sketch = CreateSketch(Obj3dType.o3d_sketch, false, _sketchX, _sketchY, _sketchZ);

            _centreCircleX = 0;
            _centreCircleY = 0;
            _circleRadius = _width / 8 * 3;
            CreateCircle(_sketch, _centreCircleX, _centreCircleY, _circleRadius);

            _centreCircleX = 0;
            _centreCircleY = 0;
            _circleRadius = _width / 2;
            CreateCircle(_sketch, _centreCircleX, _centreCircleY, _circleRadius);
            _sketch.EndEdit();

            PressOutSketch(_sketch, _wallThickness, false, 0);

            _filletX = _width / 2;
            _filletY = 0;
            _filletZ = _height / 4 * 3 + _wallThickness;

            CreateEdgeFillet(_filletX, _filletY, _filletZ, _wallThickness);
        }

        private void BuildBottleTop()
        {
            _sketchX = _width / 2 - _wallThickness - 2;
            _sketchY = 0;
            _sketchZ = _height / 4 * 3 + _wallThickness;
            _sketch = CreateSketch(Obj3dType.o3d_sketch, false, _sketchX, _sketchY, _sketchZ);

            _centreCircleX = 0;
            _centreCircleY = 0;
            _circleRadius = _width / 8 * 3;
            CreateCircle(_sketch, _centreCircleX, _centreCircleY, _circleRadius);
            _sketch.EndEdit();

            double angle = (Math.Atan(((_coverRadius / 2 - _width / 8 * 3 - _wallThickness) / (_height / 4))) / Math.PI * 180);
            PressOutSketchThickness(_sketch, _height / 4, _wallThickness, true, angle);
            CreateEdgeFillet(_coverRadius / 2, 0, _height + _wallThickness, _wallThickness / 2);
            CreateEdgeFillet(_coverRadius / 2 - _wallThickness, 0, _height + _wallThickness, _wallThickness / 2);
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

            var _sketch = (ksEntity)_connector
                .KsPart
                .NewEntity((short)Obj3dType.o3d_sketch);

            var sketchDefinition = (ksSketchDefinition)_sketch.GetDefinition();
            if (!isFirstSketch)
            {
                ksEntityCollection iCollection = 
                    _connector.KsPart.EntityCollection((short)Obj3dType.o3d_face);
                iCollection.SelectByPoint(x, y, z);
                plane = iCollection.First();
            }
            sketchDefinition.SetPlane(plane);

            _sketch.Create();
            return sketchDefinition;
        }

        private void CreateCircle(ksSketchDefinition _sketch, double centreX, double centreY, double radius)
        {
            var circle = (ksCircleParam)_connector
                .Kompas
                .GetParamStruct((short)StructType2DEnum.ko_CircleParam);

            circle.style = 1;
            var doc2D = (ksDocument2D)_sketch.BeginEdit();
            doc2D.ksCircle(centreX, centreY, radius, circle.style);
            _sketch.EndEdit();
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
    }
}
