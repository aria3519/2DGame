using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct GroundData
{
    public float xPos;
    public float width;
}
public class SpritScroll : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float scrollSpeed = 5f;

    SpriteRenderer background;
    Vector2 offset = Vector2.zero;

    [SerializeField] SpriteRenderer[] grounds;
    [SerializeField] float bounds = 5f;

    GroundData[] groundDatas;
    float halfWidth = 0;
    float prePosX = 0;
    private void Start()
    {
        var woridScreenHeight = Camera.main.orthographicSize * 2.0f;
        var woridScreenWidth = woridScreenHeight / Screen.height * Screen.width;
        background = GetComponent<SpriteRenderer>();
        if(background)
        {
            background.drawMode = SpriteDrawMode.Tiled;
            var size = background.size;
            size.x = woridScreenWidth;
            background.size = size;
        }

        halfWidth = woridScreenWidth * 0.5f;

        var count = grounds.Length;
        if(1<count)
        {
            groundDatas = new GroundData[count];
            for(int i =0;count >i ;i++)
            {
                groundDatas[i].width = grounds[i].size.x;

                if (0 < i)
                {
                    groundDatas[i].xPos = groundDatas[i - 1].xPos + bounds + groundDatas[i].width;
                    grounds[i].transform.position = Vector3.right * groundDatas[i].xPos + Vector3.down;

                }
                else groundDatas[i].xPos = grounds[i].transform.position.x;
            }
            prePosX = groundDatas[count - 1].xPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(background)
        {
            offset.x = Mathf.Repeat(Time.time * scrollSpeed * 0.01f, 1);
            background.material.mainTextureOffset = offset;
        }
        var count = grounds.Length;
        if (1 < count)
        {
            for (int i = 0; count > i; i++)
            {
                groundDatas[i].xPos -= Time.deltaTime * scrollSpeed;
                // ������Ʈ�� ī�޶� ������ ����� �ش� ������Ʈ�� ���� ������ ��ġ�� �ű��.
                // �̵� ��ġ�� ���� �����ʿ� ��ġ�� ������Ʈ�� x��ǥ + �ڽ��� �ʺ� + �� ������Ʈ���� �Ÿ�.
                if (-halfWidth >= groundDatas[i].xPos)
                {
                    groundDatas[i].xPos = prePosX + bounds + groundDatas[i].width;
                }
                // ������ ��ġ�� (x, -1, 0)�̱� ������ Vector3.down�� ���Ѵ�.
                grounds[i].transform.position = Vector3.right * groundDatas[i].xPos + Vector3.down;
                // ī�޶��� ������ ��� ��� ���� �����ʿ� ��ġ�� ������Ʈ�� ��ġ�� �����Ͽ� �д�.
                // �ڽ��� �տ� ��ġ�ߴ� ������ ���� �����ʿ� ��ġ�ϰ� �Ǵ� ������ ����.
                prePosX = groundDatas[i].xPos;
            }
        }

        if (GameMgr.Instance.isDead) return;
    }

   
}