# �e�X�g�쓮�J���̗��K�p

## ���e

�Ɩ��t���[
![Flow](assets/flow.png)

�������ʓI�ȍw�������ł�
�����S���J������K�v�͂���܂��񂪁A�������������₷���Ƃ��납����|����Ƃ����ł��傤

## �e�X�g�쓮�J���ɂ���
![Tdd Image](assets/tdd-image.png)

## CustomerRepositorySampleTest�ɂ���
TDD�̗����̂��߂�Branch���g�p���ăe�X�g�쓮�̎菇�������Ă��܂�

��{�I��Branch���Ŏ菇�������Ă����܂�

��)

1. Branch��: customer-create ---> �ŏ��ɍ��A�[�L�e�N�`��
2. Branch��: customer-01 ---> �X�e�b�v1
3. Branch��: customer-02 ---> �X�e�b�v2

������������Ȋ����Ńu�����`���쐬���Ă����܂�
�ŐV��main�ƂȂ銴���ł�

## �e�v���W�F�N�g�ɂ���
DDD�̃t�����g��������3�w���C���[�\���ɂ��Ă��܂��B
�r�W�l�X���W�b�N��Domain�ɁADB��Repository�AAPI���A�v���P�[�V�����w�ƂȂ��Ă��܂��B

�{���I�ɂ�Domain�w���J������̂�������������܂��񂪁A����Ă��Ȃ��ꍇ�́A��̗�ɂ���悤�ɁARepository�w��TDD�ō쐬����̂������Ǝv���܂��B

����Ă�����ADomain�w���J������̂������ł����A�̏ꍇRepository��Moq�Ȃǂ��g�p���ă��b�N������̂��悢�Ǝv���܂��B

�Ō�ɃA�v���P�[�V�����w�Ń��b�N���g�p���Ă�TDD�J���ł��B

�����āA���ׂĂ̒P�̃e�X�g���쐬�ŗ�����A����Integration(or Functional)�e�X�g�쐬�ƂȂ�܂����A��{�I�ɂ�API����Mock���g�p�����ɍ�邩�A����Ƃ�Repository��Mock�ɂ��邩�ǂ��炩�ō�邱�ƂɂȂ�܂��B
