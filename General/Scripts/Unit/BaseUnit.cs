using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LSemiRoguelike
{
    [DisallowMultipleComponent]
    public class BaseUnit : MonoBehaviour, IHaveInfo
    {
        [Header("Info")]
        [SerializeField] protected uint _id;
        [SerializeField] protected string _name;
        public uint ID => _id;
        public string Name => _name;

        [Header("Status")]
        [SerializeField] protected Status _maxStatus;
        [SerializeField] protected Ability _initAbility;
        [SerializeField] protected Condition _resist;

        [SerializeField] protected Renderer _renderer;
        [SerializeField] protected Sprite _sprite;

        private void OnValidate()
        {
            if (_renderer && _renderer.sharedMaterial && _sprite)
            {
                _renderer.sharedMaterial.mainTexture = _sprite.texture;
            }
        }

        protected Status _status;
        protected Ability _ability;
        protected Condition _condition;
        protected BaseContainer _container;

        public Status TotalStatus => _status;
        public Status MaxStatus => _maxStatus;
        public Ability TotalAbility => _ability;
        public Condition TotalCondition => _condition;
        public BaseContainer Container => _container;


        public void Init(BaseContainer container)
        {
            _container = container;
            Init();
        }

        protected virtual void Init()
        {
            _status.hp = _maxStatus.hp;
            SetAbility();
            _condition = new Condition();
        }

        public virtual bool IsDead => _status.hp <= 0;

        public virtual void SetAbility()
        {
            _ability = _initAbility;
        }

        public virtual Effect SetEffect(Effect effect)
        {
            effect.status.hp += effect.status.hp*_ability.attackMulti;
            if(effect.status.hp > 0)
                effect.status.hp += _ability.attackIncrese;
            else if(effect.status.hp < 0)
                effect.status.hp -= _ability.attackIncrese;

            return effect;
        }

        public virtual void GetEffect(Effect effect)
        {
            Effect finalEffect = new Effect();
            //status
            {
                var beforeSts = _status;
                _status.shield += effect.status.shield;
                _status.shield = _status.shield > MaxStatus.shield ? MaxStatus.shield : _status.shield;
                _status.shield = _status.shield < 0 ? 0 : _status.shield;

                if (effect.status.hp < 0)
                {
                    var dmg = (effect.status.hp + _ability.damageReduce) * _ability.damageMulti;
                    _status.shield += dmg < 0 ? dmg : -1;
                    if (_status.shield < 0)
                    {
                        _status.hp += _status.shield;
                        _status.shield = 0;
                    }
                }
                else if (effect.status.hp > 0)
                {
                    _status.hp += effect.status.hp;
                    if (_status.hp > _maxStatus.hp)
                        _status.hp = _maxStatus.hp;
                }
                finalEffect.status = beforeSts - _status;
            }
            //Condition
            {

            }
        }


        [MenuItem("GameObject/Dice Rogue Like/Base Unit", false, 10)]
        static void CreateBaseUnit(MenuCommand menuCommand)
        {
            var obj = new GameObject("BaseUnit", typeof(BaseUnit));

            var renderer = GameObject.CreatePrimitive(PrimitiveType.Quad);
            renderer.name = "Renderer";
            renderer.transform.SetParent(obj.transform);
            DestroyImmediate(renderer.GetComponent<Collider>());
            obj.GetComponent<BaseUnit>()._renderer = renderer.GetComponent<Renderer>();
            renderer.GetComponent<Renderer>().sharedMaterial = MaterialManager.UnitMaterial;
            renderer.GetComponent<Renderer>().sharedMaterial.mainTexture = Texture2D.whiteTexture;

            GameObjectUtility.SetParentAndAlign(obj, menuCommand.context as GameObject);

            Undo.RegisterCreatedObjectUndo(obj, "Create" + obj.name);

            Selection.activeObject = obj;
        }

        [MenuItem("GameObject/Dice Rogue Like/Skill Unit", false, 10)]
        static void CreateSkillUnit(MenuCommand menuCommand)
        {
            var obj = new GameObject("SkillUnit", typeof(SkillUnit));

            var renderer = GameObject.CreatePrimitive(PrimitiveType.Quad);
            renderer.name = "Renderer";
            renderer.transform.SetParent(obj.transform);
            DestroyImmediate(renderer.GetComponent<Collider>());
            obj.GetComponent<BaseUnit>()._renderer = renderer.GetComponent<Renderer>();
            renderer.GetComponent<Renderer>().material = MaterialManager.UnitMaterial;
            renderer.GetComponent<Renderer>().sharedMaterial.mainTexture = Texture2D.whiteTexture;

            GameObjectUtility.SetParentAndAlign(obj, menuCommand.context as GameObject);

            Undo.RegisterCreatedObjectUndo(obj, "Create" + obj.name);

            Selection.activeObject = obj;
        }

        [MenuItem("GameObject/Dice Rogue Like/Player Unit", false, 10)]
        static void CreatePlayerUnit(MenuCommand menuCommand)
        {
            var obj = new GameObject("PlayerUnit", typeof(PlayerUnit));

            var renderer = GameObject.CreatePrimitive(PrimitiveType.Quad);
            renderer.name = "Renderer";
            renderer.transform.SetParent(obj.transform);
            DestroyImmediate(renderer.GetComponent<Collider>());
            obj.GetComponent<BaseUnit>()._renderer = renderer.GetComponent<Renderer>();
            renderer.GetComponent<Renderer>().material = MaterialManager.UnitMaterial;
            renderer.GetComponent<Renderer>().sharedMaterial.mainTexture = Texture2D.whiteTexture;


            GameObjectUtility.SetParentAndAlign(obj, menuCommand.context as GameObject);

            Undo.RegisterCreatedObjectUndo(obj, "Create" + obj.name);

            Selection.activeObject = obj;
        }
    }
}