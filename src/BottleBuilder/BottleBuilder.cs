using System;
using KompasConnector;
using BottleParameters;
using Kompas6API5;
using Kompas6Constants;
using Kompas6Constants3D;


namespace BottleBuilder
{
    /// <summary>
    /// Class for build laboratory bottle 
    /// </summary>
    public class Builder
    {
        /// <summary>
        /// Variable for connecting with Kompas
        /// </summary>
        private Konnector _connector;

        /// <summary>
        /// Cover radius
        /// </summary>
        private double _coverRadius;

        /// <summary>
        /// Handle Base Radius
        /// </summary>
        private double _handleBaseRadius;

        /// <summary>
        /// Handle Radius
        /// </summary>
        private double _handleRadius;

        /// <summary>
        /// Handle Length
        /// </summary>
        private double _handleLength;

        /// <summary>
        /// Bottle height
        /// </summary>
        private double _height;

        /// <summary>
        /// Bottle width
        /// </summary>
        private double _width;

        /// <summary>
        /// Wall Thickness
        /// </summary>
        private double _wallThickness;

        /// <summary>
        /// Coordinate X pointing to a face or surface to be filleted
        /// </summary>
        private double _filletX;

        /// <summary>
        /// Coordinate Y pointing to a face or surface to be filleted
        /// </summary>
        private double _filletY;

        /// <summary>
        /// Coordinate Z pointing to a face or surface to be filleted
        /// </summary>
        private double _filletZ;

        /// <summary>
        /// Fillet radius
        /// </summary>
        private double _filletRadius;

        /// <summary>
        /// Coordinate X of centre circle
        /// </summary>
        private double _centreCircleX;

        /// <summary>
        /// Coordinate Y of centre circle
        /// </summary>
        private double _centreCircleY;

        /// <summary>
        /// Circle radius
        /// </summary>
        private double _circleRadius;

        /// <summary>
        /// Coordinate X pointing to a surface when will be create sketch
        /// </summary>
        private double _sketchX;

        /// <summary>
        /// Coordinate Y pointing to a surface when will be create sketch
        /// </summary>
        private double _sketchY;

        /// <summary>
        /// Coordinate Z pointing to a surface when will be create sketch
        /// </summary>
        private double _sketchZ;

        /// <summary>
        /// Variable pointing to sketch
        /// </summary>
        ksSketchDefinition _sketch;

        /// <summary>
        /// Method for building bottle
        /// </summary>
        /// <param name="konnector">Variable for connecting with Kompas</param>
        /// <param name="parameters">List of parameters</param>
        public void BuildBottle(Konnector konnector, Parameters parameters)
        {
            _connector = konnector;
            _connector.GetNewPart();

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

        /// <summary>
        /// Method for building handle
        /// </summary>
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

        /// <summary>
        /// Method for building base of the bottle
        /// </summary>
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

        /// <summary>
        /// Method for building top of the bottle
        /// </summary>
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
        /// Method for creating sketch
        /// </summary>
        /// <param name="planeType">Type of the plane</param>
        /// <param name="isFirstSketch"></param>
        /// <param name="x">Coordinate X pointing to a surface when will be create sketch</param>
        /// <param name="y">Coordinate Y pointing to a surface when will be create sketch</param>
        /// <param name="z">Coordinate Z pointing to a surface when will be create sketch</param>
        /// <returns name=sketchDefinition> Definition of the sketch</returns>
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
        /// Method for creating circle on sketch
        /// </summary>
        /// <param name="sketch">Sketch</param>
        /// <param name="centreX">Coordinate X of centre circle</param>
        /// <param name="centreY">Coordinate Y of centre circle</param>
        /// <param name="radius"> Radius of the circle</param>
        private void CreateCircle(ksSketchDefinition sketch, double centreX, double centreY, double radius)
        {
            var circle = (ksCircleParam)_connector
                .Kompas
                .GetParamStruct((short)StructType2DEnum.ko_CircleParam);

            circle.style = 1;
            var doc2D = (ksDocument2D)_sketch.BeginEdit();
            doc2D.ksCircle(centreX, centreY, radius, circle.style);
            sketch.EndEdit();
        }

        /// <summary>
        /// Extrude from sketch
        /// </summary>
        /// <param name="sketch">Sketch</param>
        /// <param name="thickness">Thickness of extrude</param>
        /// <param name="side">Side</param>
        /// <param name="draftValue">Draft value</param>
        private void PressOutSketch(
            ksSketchDefinition sketch,
            double thickness, bool side, double draftValue)
        {
            var extrusionEntity = (ksEntity)_connector
                .KsPart
                .NewEntity((short)Obj3dType.o3d_bossExtrusion);

            var extrusionDefinition = (ksBossExtrusionDefinition)extrusionEntity
                .GetDefinition();
            
            extrusionDefinition.SetSideParam(side, 0, thickness);

            extrusionDefinition.SetSketch(sketch);
            ExtrusionParam extrusionParam =  extrusionDefinition.ExtrusionParam();
            extrusionParam.depthNormal = thickness;
            extrusionParam.draftValueNormal = draftValue;

            extrusionEntity.Create();
        }

        /// <summary>
        /// Extrude a thin-walled feature from sketch
        /// </summary>
        /// <param name="sketch">Sketch</param>
        /// <param name="height">Height of extrude</param>
        /// <param name="wallThickness">Thickness of extrude</param>
        /// <param name="side">Side</param>
        /// <param name="draftValue">Draft value</param>
        private void PressOutSketchThickness( ksSketchDefinition sketch,
            double height, double wallThickness, bool side, double draftValue)
        {

            var extrusionEntity = (ksEntity)_connector
                .KsPart
                .NewEntity((short)Obj3dType.o3d_baseExtrusion);
            
            var extrusionDefinition = (ksBaseExtrusionDefinition)extrusionEntity
                .GetDefinition();
            
            extrusionDefinition.SetSideParam(side, 0, height);
            extrusionDefinition.SetThinParam(true, 0, wallThickness);
            
            
            extrusionDefinition.SetSketch(sketch);
            ExtrusionParam extrusionParam = extrusionDefinition.ExtrusionParam();
            extrusionParam.draftValueNormal = draftValue;
            extrusionEntity.Create();
        }

        /// <summary>
        /// Add fillet in fillet array
        /// </summary>
        /// <param name="radius">Fillet radius</param>
        /// <returns name="filletEntity">Fillet entity</returns>
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

        /// <summary>
        /// Create сhamfer
        /// </summary>
        /// <param name="x">Coordinate X pointing to a face to be сhamfered</param>
        /// <param name="y">Coordinate Y pointing to a face to be сhamfered</param>
        /// <param name="z">Coordinate Z pointing to a face to be сhamfered</param>
        /// <param name="distance1"></param>
        /// <param name="distance2"></param>
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

        /// <summary>
        /// Create fillet of the face
        /// </summary>
        /// <param name="x">Coordinate X pointing to a face or surface to be filleted</param>
        /// <param name="y">Coordinate Y pointing to a face or surface to be filleted</param>
        /// <param name="z">Coordinate Z pointing to a face or surface to be filleted</param>
        /// <param name="radius">Fillet radius</param>
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

        /// <summary>
        /// Create fillet of the edge
        /// </summary>
        /// <param name="x">Coordinate X pointing to a face or surface to be filleted</param>
        /// <param name="y">Coordinate Y pointing to a face or surface to be filleted</param>
        /// <param name="z">Coordinate Z pointing to a face or surface to be filleted</param>
        /// <param name="radius">Fillet radius</param>
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
