using KompasConnector;
using BottleParameters;
using Kompas6API5;
using Kompas6Constants;
using Kompas6Constants3D;
using BottleParameters;

namespace BottleBuilder
{
    public class Builder
    {
        private Konnector _connector;

        public void BuildBottle(Konnector konnector, Parameters parameters)
        {
            _connector = konnector;

            var sketch = CreateSketch(Obj3dType.o3d_planeXOY);

            var doc2D = (ksDocument2D)sketch.BeginEdit();

            var circle = (ksCircleParam) _connector
                .Kompas
                .GetParamStruct((short) StructType2DEnum.ko_CircleParam);

            circle.xc = 0;
            circle.yc = 0;
            circle.style = 1;
            circle.rad = parameters.FindParameter(ParameterTypeEnum.Width, parameters.ParametersList) / 2;

            doc2D.ksCircle(circle.xc, circle.yc, circle.rad, circle.style);

            //Выйти из режима редактирования эскиза
            sketch.EndEdit();

            //Выдавливание детали
           PressOutSketch(sketch, parameters.FindParameter(ParameterTypeEnum.WallThickness, parameters.ParametersList) , false);

            PressOutSketchThickness(sketch, parameters.FindParameter(ParameterTypeEnum.Height, parameters.ParametersList) * 3 / 4, 
                parameters.FindParameter(ParameterTypeEnum.WallThickness, parameters.ParametersList), true);
        }

        /// <summary>
        /// Создание эскиза
        /// </summary>
        /// <param name="planeType">Выбор плоскости</param>
        /// <returns>ksSketchDefinition</returns>
        public ksSketchDefinition CreateSketch(Obj3dType planeType)
        {
            //Элемент модели по умолчанию
            var plane = (ksEntity)_connector
                .KsPart
                .GetDefaultEntity((short)planeType);

            //Создать новый интерфейс объекта и получить указатель на него
            var sketch = (ksEntity)_connector
                .KsPart
                .NewEntity((short)Obj3dType.o3d_sketch);

            //Получить указатель на интерфейс параметров объектов или элементов
            var sketchDefinition = (ksSketchDefinition)sketch.GetDefinition();

            //Изменить базовую плоскость эскиза
            sketchDefinition.SetPlane(plane);

            //Создать объект в модели
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
            double thickness, bool side)
        {
            //Создать новый интерфейс объекта и получить указатель на него
            var extrusionEntity = (ksEntity)_connector
                .KsPart
                .NewEntity((short)Obj3dType.o3d_bossExtrusion);

            //Интерфейс приклеенного элемента выдавливания
            var extrusionDefinition = (ksBossExtrusionDefinition)extrusionEntity
                .GetDefinition();

            //Установить параметры выдавливания в одном направлении
            //side - направление (true - прямое направление)
            //тип выдавливания (0 - строго на глубину)
            //глубина выдавливания
            extrusionDefinition.SetSideParam(side, 0, thickness);

            //Изменить указатель на интерфейс эскиза элемента
            extrusionDefinition.SetSketch(sketchDefinition);

            //Создать объект в модели
            extrusionEntity.Create();
        }

        /// <summary>
        /// Выдавливание по эскизу
        /// </summary>
        /// <param name="sketchDefinition">Эскиз</param>
        /// <param name="thickness">Толщина</param>
        private void PressOutSketchThickness(
            ksSketchDefinition sketchDefinition,
            double thickness, double wallThickness, bool side)
        {
            //Создать новый интерфейс объекта и получить указатель на него
            var extrusionEntity = (ksEntity)_connector
                .KsPart
                .NewEntity((short)Obj3dType.o3d_baseExtrusion);

            //Интерфейс приклеенного элемента выдавливания
            var extrusionDefinition = (ksBaseExtrusionDefinition)extrusionEntity
                .GetDefinition();

            //Установить параметры выдавливания в одном направлении
            //side - направление (true - прямое направление)
            //тип выдавливания (0 - строго на глубину)
            //глубина выдавливания
            //extrusionDefinition.SetSideParam(side, 1, thickness);
            extrusionDefinition.SetSideParam(side, 0, thickness);
            extrusionDefinition.SetThinParam(true, 0, wallThickness);
             
            //Изменить указатель на интерфейс эскиза элемента
            extrusionDefinition.SetSketch(sketchDefinition);

            //Создать объект в модели
            extrusionEntity.Create();
        }
    }
}
