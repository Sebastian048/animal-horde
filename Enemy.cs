using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float health;
    int startHealth;

    public bool target;

    public bool arenaGameMode;

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    public bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool playerInSightRange,playerInAttackRange,enemyHit;

    public int enemyType;

    public float damage;

    public WaveSpawner wave;

    Renderer eyesMat;

    public bool targetDown;
    public float targetRotation;
    bool revived = true;

    public int gameMode;
    public int targetNum;

    public bool debug;

    public Animator enemyAnim;

    public float damageMultiplier;

    private GameObject dif;

    int points;

    public GameObject killEffect;
    public Material killMat;

    public PlayerHealth stats;

    public float difficultyMultiplier;

    public float transaprencyMultiplier;

    public Slider healthBar;
    public float targetValueHealthBar;

    public bool hasAttacked;

    public bool checkAttackForAnimations;
    public bool checkWalkForAnimations;

    public bool attacking;

    public bool bossEnemy;

    private void Awake () {

        if (target == false) {

            if (FindObjectOfType<Options>().healthBarNum == 0) {
            healthBar.gameObject.SetActive(false);
                } else {
            healthBar.gameObject.SetActive(true);
            }

        player = GameObject.Find("FirstPersonPlayer").transform;
        agent = GetComponent<NavMeshAgent>();
        startHealth = (int)health;
        
        points = (int)(health/3);
        if (bossEnemy == true) {
            points = points * FindObjectOfType<WaveSpawner>().enemiesThatWereRemoved * 2;
        }

        dif = GameObject.Find("DifficultySelection");
        if (dif.GetComponent<DifficultySelection>().difficulty == "easy") {
            damage = (int) (damage / 2.5f);
            health = (int) (health / 2.5f);

            difficultyMultiplier = 0.3f;

            gameObject.GetComponent<NavMeshAgent>().speed = gameObject.GetComponent<NavMeshAgent>().speed / 1.4f;
        } else if (dif.GetComponent<DifficultySelection>().difficulty == "medium") {
            damage = (int) (damage / 1.5f);
            health = (int) (health / 1.5f);

            difficultyMultiplier = 0.75f;
            
            gameObject.GetComponent<NavMeshAgent>().speed = gameObject.GetComponent<NavMeshAgent>().speed / 1.2f;
        } else if (dif.GetComponent<DifficultySelection>().difficulty == "hard") {
            damage = (int) (damage * 1f);
            health = (int) (health * 1);

            difficultyMultiplier = 1.5f;
        }

        InvokeRepeating("ChasePlayer", 0, 0.1f);
        }
    }

    public void TakeDamage (int amount) {
        if (target == false) {
            if (health > 0) {
                FindObjectOfType<HitMarkerManager>().HitMarkerSpawn();
                FindObjectOfType<HitDamageText>().SpawnDamageText(amount);
                FindObjectOfType<SoundManager>().HitMarker();
                FindObjectOfType<PlayerHealth>().AddDamageDealt((int)damage);

                health -= amount;
                health = health - amount * damageMultiplier;

                healthBar.value = health;
                
                enemyHit = true;
            

                if (health <= 0f) {
                FindObjectOfType<WaveSpawner>().EnemyIsAliveTest();
                FindObjectOfType<PlayerHealth>().AddEnemiesEliminated();
                FindObjectOfType<PlayerHealth>().AddPointsAccumulated(points);
                FindObjectOfType<Shop>().AddPoints(points);

                Die();
                }
            }
        } else {
            if (health > 0 && revived == true) {
                FindObjectOfType<HitMarkerManager>().HitMarkerSpawn();
                FindObjectOfType<HitDamageText>().SpawnDamageText(amount);
                FindObjectOfType<SoundManager>().HitMarker();

                health -= amount;
                if (debug == true) {
                    Destroy(gameObject);
                    return;
                }
                if (health <= 0f && gameMode == 0 && arenaGameMode == false) {
                    targetDown = true;
                    revived = false;
                    FindObjectOfType<PracticeGameModeManager>().AddPoint();
                } else if (gameMode == 1) {
                    FindObjectOfType<PracticeGameModeManager>().AddPoint();
                    this.transform.parent.gameObject.SetActive(false);
                    FindObjectOfType<TargetGameModes>().TargetDown(targetNum);
                } else if (health <= 0f && gameMode == 2 && arenaGameMode == false) {
                    targetDown = true;
                    revived = false;
                    FindObjectOfType<PracticeGameModeManager>().AddPoint();
                }
            }
        }
    }

    /*private void OnTriggerStay(Collider other)
    {
        if (other.name == "PlayerCol") {
            AttackPlayerVoid();

            if (checkAttackForAnimations == false) {
                enemyAnim.SetBool("Run", false);
                enemyAnim.SetBool("Walk", false);
                enemyAnim.SetBool("Attack", true);
                    
                checkWalkForAnimations = false;
            }

            checkAttackForAnimations = true;

            attacking = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "PlayerCol") {
            attacking = false;
        }
    }*/

    public void healthBarAcitvation (int healthBarNumGiven) {
        if (healthBarNumGiven == 0) {
            healthBar.gameObject.SetActive(false);
        } else {
            healthBar.gameObject.SetActive(true);
        }
    }

    void FixedUpdate () {
            if (target == false) {

            //EnemyAnimations();

            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            /*if (!playerInSightRange && !playerInAttackRange && !enemyHit || ((playerInSightRange || enemyHit) && !playerInAttackRange)) {
                ChasePlayer();

                hasAttacked = false;

                if (checkWalkForAnimations == false) {
                    enemyAnim.SetBool("Run", false);
                    enemyAnim.SetBool("Walk", true);
                    enemyAnim.SetBool("Attack", false);
                    
                    checkAttackForAnimations = false;
                }

                checkWalkForAnimations = true;
            }*/
            
            if (playerInAttackRange && playerInSightRange) {
                AttackPlayerVoid();

                if (checkAttackForAnimations == false) {
                    enemyAnim.SetBool("Run", false);
                    enemyAnim.SetBool("Walk", false);
                    enemyAnim.SetBool("Attack", true);
                    
                    checkWalkForAnimations = false;
                }

                checkAttackForAnimations = true;
            }
        }

        if (target == true) {
            if (gameMode == 0) {
                if (targetDown == true) {
                if (targetRotation <= 90) {
                    targetRotation = targetRotation + 30 * Time.deltaTime;
                } else {
                    targetDown = false;
                }
            } else {
                if (targetRotation >= 0) {
                    targetRotation = targetRotation - 30 * Time.deltaTime;
                } else {
                    if (revived == false) {
                        revived = true;
                    health = 100;
                    }
                }
            }

            this.transform.parent.transform.localRotation = Quaternion.Euler(targetRotation, 0, 0);

            } else if (gameMode == 2) {
                if (targetDown == true) {
                    this.GetComponent<MeshRenderer>().enabled = false;
                    StartCoroutine (RevivingTarget());
                } else {
                if (revived == false) {
                        revived = true;
                        health = 100;
                        this.GetComponent<MeshRenderer>().enabled = true;
                    }
                }
            }
        
        }
    }

    IEnumerator RevivingTarget () {

        yield return new WaitForSeconds(3);

        targetDown = false;
    }

    /*public void EnemyAnimations () {
        if (playerInSightRange) {
            enemyAnim[enemyType].SetBool("Run", true);
            enemyAnim[enemyType].SetBool("Walk", false);
            enemyAnim[enemyType].SetBool("Attack", false);
        }
        if (playerInAttackRange) {
            enemyAnim[enemyType].SetBool("Run", false);
            enemyAnim[enemyType].SetBool("Walk", false);
            enemyAnim[enemyType].SetBool("Attack", true);
        }
        if (!playerInSightRange) {
            enemyAnim[enemyType].SetBool("Run", false);
            enemyAnim[enemyType].SetBool("Walk", true);
            enemyAnim[enemyType].SetBool("Attack", false);
        }
    }*/

    public void Die () {
        int gunUsing = FindObjectOfType<GunManager>().gun;
        FindObjectOfType<SkinShop>().AddAchievement(gunUsing);

        killEffect = GameObject.Find("killImpact");
        killEffect.GetComponent<Renderer>().sharedMaterial.color = killMat.color;
        Renderer mat = gameObject.transform.GetComponent<Renderer>();
        GameObject diePlayerEffect = Instantiate(killEffect, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(diePlayerEffect, 5);

        Destroy(gameObject);
    }

    /*private void Patroling () {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet) 
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f) 
            walkPointSet = false;
    }*/

    /*private void SearchWalkPoint () {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }*/

    private void ChasePlayer () {
        if (attacking == false) {

            agent.SetDestination(player.position);

            hasAttacked = false;

            if (checkWalkForAnimations == false) {
                enemyAnim.SetBool("Run", false);
                enemyAnim.SetBool("Walk", true);
                enemyAnim.SetBool("Attack", false);
                    
                checkAttackForAnimations = false;
            }

            checkWalkForAnimations = true;
            }
    }

    private void AttackPlayer () {
        if (!alreadyAttacked) {

            FindObjectOfType<PlayerHealth>().RecieveDamage((int)damage, this.gameObject);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

            hasAttacked = true;
        }
    }

    private void ResetAttack () {
        alreadyAttacked = false;
    }

    private void AttackPlayerVoid () {
        agent.SetDestination(transform.position);

        if (hasAttacked == true) {
            Invoke("AttackPlayer",  timeBetweenAttacks);
        } else {
            AttackPlayer();
        }
    }

    public void UpdateEnemyDifficulty () {

        if (enemyType == 0) {
            damage = damage + (wave.waveNum - 1) * (int)(2 * difficultyMultiplier);
        
            health = health + ((wave.waveNum - 1) * (int)(25 * difficultyMultiplier));
        } else if (enemyType == 1) {
            damage = damage + (wave.waveNum - 1) * (int)(2 * difficultyMultiplier);
        
            health = health + ((wave.waveNum - 1) * (int)(20 * difficultyMultiplier));
        } else if (enemyType == 2) {
            damage = damage + (wave.waveNum - 1) * (int)(3 * difficultyMultiplier);
        
            health = health + ((wave.waveNum - 1) * (int)(35 * difficultyMultiplier));
        } else if (enemyType == 3) {
            damage = damage + (wave.waveNum - 1) * (int)(4 * difficultyMultiplier);
        
            health = health + ((wave.waveNum - 1) * (int)(50 * difficultyMultiplier));
        } else if (enemyType == 4) {
            damage = damage + (wave.waveNum - 1) * (int)(5 * difficultyMultiplier);
        
            health = health + ((wave.waveNum - 1) * (int)(40 * difficultyMultiplier));
        } else if (enemyType == 5) {
            damage = damage + (wave.waveNum - 1) * (int)(4 * difficultyMultiplier);
        
            health = health + ((wave.waveNum - 1) * (int)(75 * difficultyMultiplier));
        } else if (enemyType == 6) {
            damage = damage + (wave.waveNum - 1) * (int)(5 * difficultyMultiplier);
        
            health = health + ((wave.waveNum - 1) * (int)(60 * difficultyMultiplier));
        } else if (enemyType == 7) {
            damage = damage + (wave.waveNum - 1) * (int)(7 * difficultyMultiplier);
        
            health = health + ((wave.waveNum - 1) * (int)(40 * difficultyMultiplier));
        } else if (enemyType == 8) {
            damage = damage + (wave.waveNum - 1) * (int)(4 * difficultyMultiplier);
        
            health = health + ((wave.waveNum - 1) * (int)(60 * difficultyMultiplier));
        } else if (enemyType == 9) {
            damage = damage + (wave.waveNum - 1) * (int)(5 * difficultyMultiplier);
        
            health = health + ((wave.waveNum - 1) * (int)(75 * difficultyMultiplier));
        } else if (enemyType == 10) {
            damage = damage + (wave.waveNum - 1) * (int)(4 * difficultyMultiplier);
        
            health = health + ((wave.waveNum - 1) * (int)(100 * difficultyMultiplier));
        }

        if (bossEnemy == true) {
            damage = (int)(damage * (FindObjectOfType<WaveSpawner>().enemiesThatWereRemoved * 0.6));
            health = (int)(health * (FindObjectOfType<WaveSpawner>().enemiesThatWereRemoved * 0.8));
        }

        healthBar.maxValue = health;
        healthBar.value = health;
        targetValueHealthBar = health;
    }

    public void multiplyDamage () {
        damageMultiplier = damageMultiplier + 1.05f;
    }
}