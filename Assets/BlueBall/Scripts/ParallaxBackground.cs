using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{

    public Camera cameraBG;
    public SpriteRenderer spriteBG;
    public Vector2 smoothMovement = Vector2.zero;
    private Vector3 lastCameraPosition;

    private float textureUnitSizeX;
    private float textureUnitSizeY;
    // Start is called before the first frame update
    void Start()
    {
        lastCameraPosition = cameraBG.transform.position;
        Debug.Log(spriteBG.sprite.pixelsPerUnit);
        textureUnitSizeX = spriteBG.sprite.texture.width / spriteBG.sprite.pixelsPerUnit;
        textureUnitSizeY = spriteBG.sprite.texture.height / spriteBG.sprite.pixelsPerUnit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        Vector3 detaMovement = cameraBG.transform.position - lastCameraPosition;
        transform.position += new Vector3(detaMovement.x * smoothMovement.x,detaMovement.y * smoothMovement.y,0f);
        lastCameraPosition = cameraBG.transform.position;


        //if (Mathf.Abs(cameraBG.transform.position.x - transform.position.x) >= textureUnitSizeX) {
        //    float offsetPosX = (cameraBG.transform.position.x - transform.position.x) % textureUnitSizeX;
        //    transform.position = new Vector3(cameraBG.transform.position.x + offsetPosX,transform.position.y);
        //}

        //if (Mathf.Abs(cameraBG.transform.position.y - transform.position.y) >= textureUnitSizeY)
        //{
        //    float offsetPosY = (cameraBG.transform.position.y - transform.position.y) % textureUnitSizeY;
        //    transform.position = new Vector3(transform.position.x,cameraBG.transform.position.y + offsetPosY);
        //}
    }
}
