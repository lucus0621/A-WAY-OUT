using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pack : MonoBehaviour
{

    public List<ItemEntity> items = null;
    public int maxItem = 10;

    // Use this for initialization
    void Start()
    {
        items = new List<ItemEntity>();
    }

    //PickUP
    public ObjectItem getItem(ObjectItem item)
    {
        //TempObject
        ItemEntity itemEntity = ItemEntity.FillData(item);

        //can't combine
        if (!itemEntity.IsCanAdd)
        {

            if (items.Count < maxItem)
            {
                //Got item
                items.Add(itemEntity);
                item.count = 0;
            }
            else
            {
                //can get item
            }
        }
        else
        {
            if (items.Count < 1)
            {
                items.Add(itemEntity);
                item.count = 0;
            }
            else
            {
                foreach (ItemEntity currItem in items)
                {

                    if (currItem.ObjId.Equals(itemEntity.ObjId) && currItem.Count < currItem.MaxAdd)
                    {
                        //add number
                        currItem.Count = currItem.Count + itemEntity.Count;
                        //bigger than Max
                        if (currItem.Count - currItem.MaxAdd > 0)
                        {
                            //the rest of pickUp item number changed
                            item.count = currItem.Count - currItem.MaxAdd;
                            itemEntity.Count = item.count;
                            //pack full
                            currItem.Count = currItem.MaxAdd;
                        }
                        else
                        {
                            // The stack number is ont exceeded, got item
                            item.count = 0;
                        }
                    }
                    else
                    {
                        //if it's not same item, keep show 
                        continue;
                    }
                }
                //if item number bigger than max, then go to other ,if all full, then can get item
                if (item.count > 0 && items.Count < maxItem)
                {
                    items.Add(itemEntity);
                    item.count = 0;
                }
            }
        }
        return item;
    }

    //Show
    public void showPack()
    {
        string show = "Items£º\n";
        int i = 0;
        foreach (ItemEntity currItem in items)
        {
            show += ++i + " [" + currItem.ObjName + "], Number: " + currItem.Count + "\n";
        }
        Debug.Log(show);
    }
}
