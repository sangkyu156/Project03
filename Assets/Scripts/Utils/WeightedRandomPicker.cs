using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
/*
[����ġ ���� �̱�]

- ���׸��� ���� �������� Ÿ���� ������ ��üȭ�Ͽ� ����Ѵ�.
- �ߺ��Ǵ� �������� ������ ��ųʸ��� �����Ͽ���.
- ����ġ�� 0���� ���� ��� ���ܸ� ȣ���Ѵ�.

- double SumOfWeights : ��ü �������� ����ġ ��(�б� ���� ������Ƽ)

- void Add(T, double) : ���ο� ������-����ġ ���� �߰��Ѵ�.
- void Add(params (T, double)[]) : ���ο� ������-����ġ ���� ���� �� �߰��Ѵ�.
- void Remove(T) : ��� �������� ��Ͽ��� �����Ѵ�.
- void ModifyWeight(T, double) : ��� �������� ����ġ�� �����Ѵ�.
- void ReSeed(int) : ���� �õ带 �缳���Ѵ�.

- T GetRandomPick() : ���� ������ ��Ͽ��� ����ġ�� ����Ͽ� �������� �׸� �ϳ��� �̾ƿ´�.
- T GetRandomPick(double) : �̹� ���� Ȯ�� ���� �Ű������� �־�, �ش�Ǵ� �׸� �ϳ��� �̾ƿ´�.

- ReadonlyDictionary<T, double> GetItemDictReadonly() : ��ü ������ ����� �б����� �÷������� �޾ƿ´�.
- ReadonlyDictionary<T, double> GetNormalizedItemDictReadonly()
: ��ü �������� ����ġ ������ 1�� �ǵ��� ����ȭ�� ������ ����� �б����� �÷������� �޾ƿ´�.
*/

namespace WRandom
{
    /// <summary> ����ġ ���� �̱� </summary>
    public class WeightedRandomPicker<T>
    {
        /// <summary> ��ü �������� ����ġ �� </summary>
        public double SumOfWeights
        {
            get
            {
                CalculateSumIfDirty();
                return _sumOfWeights;
            }
        }

        private System.Random randomInstance;
        private readonly Dictionary<T, double> itemWeightDict;
        private readonly Dictionary<T, double> normalizedItemWeightDict; // Ȯ���� ����ȭ�� ������ ���

        /// <summary> ����ġ ���� ������ ���� �������� ���� </summary>
        private bool isDirty;
        private double _sumOfWeights;

        /***********************************************************************
        *                               Constructors
        ***********************************************************************/
        #region .
        public WeightedRandomPicker()
        {
            randomInstance = new System.Random();
            itemWeightDict = new Dictionary<T, double>();
            normalizedItemWeightDict = new Dictionary<T, double>();
            isDirty = true;
            _sumOfWeights = 0.0;
        }

        public WeightedRandomPicker(int randomSeed)
        {
            randomInstance = new System.Random(randomSeed);
            itemWeightDict = new Dictionary<T, double>();
            normalizedItemWeightDict = new Dictionary<T, double>();
            isDirty = true;
            _sumOfWeights = 0.0;
        }

        #endregion
        /***********************************************************************
        *                               Add Methods
        ***********************************************************************/
        #region .

        /// <summary> ���ο� ������-����ġ �� �߰� </summary>
        public void Add(T item, double weight)
        {
            CheckDuplicatedItem(item);
            CheckValidWeight(weight);

            itemWeightDict.Add(item, weight);
            isDirty = true;
        }

        /// <summary> ���ο� ������-����ġ �ֵ� �߰� </summary>
        public void Add(params (T item, double weight)[] pairs)
        {
            foreach (var i in pairs)
            {
                CheckDuplicatedItem(i.item);
                CheckValidWeight(i.weight);

                itemWeightDict.Add(i.item, i.weight);
            }
            isDirty = true;
        }

        #endregion
        /***********************************************************************
        *                               Public Methods
        ***********************************************************************/
        #region .

        /// <summary> ��Ͽ��� ��� ������ ���� </summary>
        public void Remove(T item)
        {
            UnityEngine.Debug.Log($"���� = {item}");
            CheckNotExistedItem(item);

            itemWeightDict.Remove(item);
            isDirty = true;

            UnityEngine.Debug.Log($"������ ������  = {item}");
        }

        /// <summary> ��� �������� ����ġ ���� </summary>
        public void ModifyWeight(T item, double weight)
        {
            CheckNotExistedItem(item);
            CheckValidWeight(weight);

            itemWeightDict[item] = weight;
            isDirty = true;
        }

        /// <summary> ���� �õ� �缳�� </summary>
        public void ReSeed(int seed)
        {
            randomInstance = new System.Random(seed);
        }

        #endregion
        /***********************************************************************
        *                               Getter Methods
        ***********************************************************************/
        #region .

        /// <summary> ���� �̱� </summary>
        public T GetRandomPick()
        {
            // ���� ���
            double chance = randomInstance.NextDouble(); // (0.0, 1.0)
            chance *= SumOfWeights;

            return GetRandomPick(chance);
        }

        /// <summary> ���� ���� ���� �����Ͽ� �̱� </summary>
        public T GetRandomPick(double randomValue)
        {
            if (randomValue < 0.0) randomValue = 0.0;
            if (randomValue > SumOfWeights) randomValue = SumOfWeights - 0.00000001;

            double current = 0.0;
            foreach (var i in itemWeightDict)
            {
                current += i.Value;

                if (randomValue < current)
                {
                    return i.Key;
                }
            }

            throw new Exception($"Unreachable - [Random Value : {randomValue}, Current Value : {current}]");
            //return itemPairList[itemPairList.Count - 1].item; // Last Item
        }

        /// <summary> ��� �������� ����ġ Ȯ�� </summary>
        public double GetWeight(T item)
        {
            return itemWeightDict[item];
        }

        /// <summary> ��� �������� ����ȭ�� ����ġ Ȯ�� </summary>
        public double GetNormalizedWeight(T item)
        {
            CalculateSumIfDirty();
            return normalizedItemWeightDict[item];
        }

        /// <summary> ������ ��� Ȯ��(�б� ����) </summary>
        public ReadOnlyDictionary<T, double> GetItemDictReadonly()
        {
            return new ReadOnlyDictionary<T, double>(itemWeightDict);
        }

        /// <summary> ����ġ ���� 1�� �ǵ��� ����ȭ�� ������ ��� Ȯ��(�б� ����) </summary>
        public ReadOnlyDictionary<T, double> GetNormalizedItemDictReadonly()
        {
            CalculateSumIfDirty();
            return new ReadOnlyDictionary<T, double>(normalizedItemWeightDict);
        }

        #endregion
        /***********************************************************************
        *                               Private Methods
        ***********************************************************************/
        #region .

        /// <summary> ��� �������� ����ġ �� ����س��� </summary>
        private void CalculateSumIfDirty()
        {
            if (!isDirty) return;
            isDirty = false;

            _sumOfWeights = 0.0;
            foreach (var pair in itemWeightDict)
            {
                _sumOfWeights += pair.Value;
            }

            // ����ȭ ��ųʸ��� ������Ʈ
            UpdateNormalizedDict();
        }

        /// <summary> ����ȭ�� ��ųʸ� ������Ʈ </summary>
        private void UpdateNormalizedDict()
        {
            normalizedItemWeightDict.Clear();
            foreach (var pair in itemWeightDict)
            {
                normalizedItemWeightDict.Add(pair.Key, pair.Value / _sumOfWeights);
            }
        }

        /// <summary> �̹� �������� �����ϴ��� ���� �˻� </summary>
        private void CheckDuplicatedItem(T item)
        {
            if (itemWeightDict.ContainsKey(item))
                throw new Exception($"�̹� [{item}] �������� �����մϴ�.");
        }

        /// <summary> �������� �ʴ� �������� ��� </summary>
        private void CheckNotExistedItem(T item)
        {
            if (!itemWeightDict.ContainsKey(item))
                UnityEngine.Debug.Log($"[{item}] �������� ��Ͽ� �������� �ʽ��ϴ�.");
        }

        /// <summary> ����ġ �� ���� �˻�(0���� Ŀ�� ��) </summary>
        private void CheckValidWeight(in double weight)
        {
            if (weight <= 0f)
                throw new Exception("����ġ ���� 0���� Ŀ�� �մϴ�.");
        }

        #endregion
    }
}